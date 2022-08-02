#import "ViewController.h"
#import "FaceView.h"
#import "Utils.h"
#import "DBConnection.h"

@interface ViewController ()

@property (weak, nonatomic) IBOutlet FaceView *faceView;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)showEnterIdDialog:(NSString *)buttonTitle withCallback:(void (^)(NSString *subjectId))callback {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Enter ID" message:nil preferredStyle:UIAlertControllerStyleAlert];

    UIAlertAction *ok = [UIAlertAction actionWithTitle:buttonTitle style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        callback(alert.textFields[0].text);
    }];

    UIAlertAction *cancel = [UIAlertAction actionWithTitle:@"Cancel" style:UIAlertActionStyleCancel handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
    }];

    [alert addAction:ok];
    [alert addAction:cancel];

    [alert addTextFieldWithConfigurationHandler:^(UITextField * _Nonnull textField) {
        [textField setPlaceholder:@"ID"];
    }];

    [self presentViewController:alert animated:YES completion:nil];
}

- (void)showLivenessModeSelectDialog {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Select Liveness Mode" message:nil preferredStyle:UIAlertControllerStyleAlert];

    NSString *noneLabel = @"None";
    NSString *passiveLabel = @"Passive";
    NSString *activeLabel = @"Active";
    NSString *passiveAndActiveLabel = @"Passive and Active";
    NSString *simpleLabel = @"Simple";
    NSString *customLabel = @"Custom";
    NSString *currentLabel = @" (current)";

    NfvcLivenessMode mode;
    NRESULT_CHECK(NFaceVerificationClientGetLivenessMode(&mode));
    switch (mode) {
        case nfvclmNone:
            noneLabel = [noneLabel stringByAppendingString:currentLabel];
            break;
        case nfvclmPassive:
            passiveLabel = [passiveLabel stringByAppendingString:currentLabel];
            break;
        case nfvclmActive:
            activeLabel = [activeLabel stringByAppendingString:currentLabel];
            break;
        case nfvclmPassiveAndActive:
            passiveAndActiveLabel = [passiveAndActiveLabel stringByAppendingString:currentLabel];
            break;
        case nfvclmSimple:
            simpleLabel = [simpleLabel stringByAppendingString:currentLabel];
            break;
        case nfvclmCustom:
            customLabel = [customLabel stringByAppendingString:currentLabel];
            break;
    }

    UIAlertAction *none = [UIAlertAction actionWithTitle:noneLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmNone));
    }];
    UIAlertAction *passive = [UIAlertAction actionWithTitle:passiveLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmPassive));
    }];
    UIAlertAction *active = [UIAlertAction actionWithTitle:activeLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmActive));
    }];
    UIAlertAction *passiveAndActive = [UIAlertAction actionWithTitle:passiveAndActiveLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmPassiveAndActive));
    }];
    UIAlertAction *simple = [UIAlertAction actionWithTitle:simpleLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmSimple));
    }];
    UIAlertAction *custom = [UIAlertAction actionWithTitle:customLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmCustom));
    }];

    [alert addAction:none];
    [alert addAction:passive];
    [alert addAction:active];
    [alert addAction:passiveAndActive];
    [alert addAction:simple];
    [alert addAction:custom];

    [self presentViewController:alert animated:YES completion:nil];
}

- (void)showSubjectSelectDialog {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Select Subject" message:nil preferredStyle:UIAlertControllerStyleAlert];
    NSMutableArray *subjects = [DBConnection loadSubjects];
    NSLog(@"subject count %lu", (unsigned long)[subjects count]);
    if ([subjects count] == 0) {
        dispatch_async(dispatch_get_main_queue(), ^{
            UIAlertController *alert = [Utils createSimpleAlert:@"Error" withMessage:@"No subjects found"];
            [self presentViewController:alert animated:YES completion:nil];
        });
        return;
    }
    for (NSString *subject in subjects) {
        UIAlertAction *action = [UIAlertAction actionWithTitle:subject style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
            [alert dismissViewControllerAnimated:YES completion:nil];
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                [self verifyTask:subject];
            });
        }];
        [alert addAction:action];
    }
    [self presentViewController:alert animated:YES completion:nil];
}

static NResult N_API previewCallback(HNfvcCapturePreview hPreviewCallback, void * pParam) {
    NfvcStatus status = nfvcsNone;
    NFloat currentYaw = 0;
    NFloat currentRoll = 0;
    CGRect cgRect = {0};
    NfvcLivenessAction livenessAction = nfvclaNone;
    NFloat targetYaw = 0;
    NByte livenessScore = 0;
    NInt pX, pY, pWidth, pHeight;
    NfvcIcaoWarnings icaoWarnings;
    CGImageRef image;
    NFaceVerificationClientCapturePreviewGetCGImageRef(hPreviewCallback, &image);
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetStatus(hPreviewCallback, &status));
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetYaw(hPreviewCallback, &currentYaw));
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetBoundingRect(hPreviewCallback, &pX, &pY, &pWidth, &pHeight));
    cgRect = CGRectMake(pX, pY, pWidth, pHeight);
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetLivenessAction(hPreviewCallback, &livenessAction));
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetLivenessTargetYaw(hPreviewCallback, &targetYaw));
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetLivenessScore(hPreviewCallback, &livenessScore));
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetIcaoWarnings(hPreviewCallback, &icaoWarnings));
    NFaceVerificationClientCapturePreviewGetRoll(hPreviewCallback, &currentRoll);
    
    @autoreleasepool {
        UIImage *uiImage = [[UIImage alloc] initWithCGImage:image];
        CGImageRelease(image);
        ViewController *viewController = (__bridge id) pParam;
        dispatch_async(dispatch_get_main_queue(), ^{
            [viewController.statusLabel setText:[NSString stringWithFormat:@"Status: %i - %@", status, [Utils faceVerificationStatusToNSString:status]]];
            [viewController.faceView setFaceImage:uiImage];
            [viewController.faceView setCurrentYaw:currentYaw];
            [viewController.faceView setCurrentRoll:currentRoll];
            [viewController.faceView setFaceBoundingRect:cgRect];
            [viewController.faceView setLivenessAction:livenessAction];
            [viewController.faceView setLivenessTargetYaw:targetYaw];
            [viewController.faceView setLivenessScore:livenessScore];
            [viewController.faceView setIcaoWarnings:icaoWarnings];
            [viewController.faceView repaintOverlay];
        });
    }

    return 0;
}

- (void)enrollTask:(NSString *)subjectId {
    NSLog(@"enrollTask started");

    NfvcStatus status = nfvcsNone;
    NInt bufferLength;
    void *buffer;

    NRESULT_CHECK(NFaceVerificationClientSetCapturePreviewCallback(previewCallback, (__bridge void *)self));
    NRESULT_CHECK(NFaceVerificationClientStartCreateTemplate(&buffer, &bufferLength));
    NSData *response = [Utils sendPostRequest:@"validate" withData:buffer withLength:bufferLength];
    NInt serverKeyLen = (NInt)[response length];
    void *serverKey = malloc(serverKeyLen);
    memcpy(serverKey, [response bytes], serverKeyLen);
    HNfvcResult resultHandle;
    NRESULT_CHECK(NFaceVerificationClientFinishOperation(serverKey, serverKeyLen, &resultHandle));
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetStatus(resultHandle, &status));
    if (status == nfvcsSuccess) {
        void *templateBuffer;
        NInt templateBufferLen;
        NRESULT_CHECK(NFaceVerificationClientResultGetTemplate(resultHandle, &templateBuffer, &templateBufferLen));
        [DBConnection save:subjectId withData:templateBuffer ofLength:templateBufferLen];
        free(templateBuffer);
    }
    NRESULT_CHECK(NFaceVerificationClientFreeHandle(&resultHandle));
    free(serverKey);

    dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertController *alert = [Utils createSimpleAlert:[NSString stringWithFormat:@"Enroll ID: %@", subjectId] withMessage:[NSString stringWithFormat:@"Enroll status: %i - %@", status, [Utils faceVerificationStatusToNSString:status]]];
        [self presentViewController:alert animated:YES completion:nil];
    });

    NSLog(@"enrollTask done");
}

- (void)verifyTask:(NSString *)subjectId {
    NSLog(@"verifyTask started");

    NfvcStatus status = nfvcsNone;
    NRESULT_CHECK(NFaceVerificationClientSetCapturePreviewCallback(previewCallback, (__bridge void *)self));
    NSData *templateData = [DBConnection loadSubjectData:subjectId];
    NInt templateLen = (NInt)[templateData length];
    void *template = malloc(templateLen);
    memcpy(template, [templateData bytes], templateLen);
    HNfvcResult resultHandle = NULL;
    NRESULT_CHECK(NFaceVerificationClientVerify(template, templateLen, &resultHandle));
    
    NRESULT_CHECK(NFaceVerificationClientCapturePreviewGetStatus(resultHandle, &status));
    NRESULT_CHECK(NFaceVerificationClientFreeHandle(&resultHandle));
    free(template);

    dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertController *alert = [Utils createSimpleAlert:[NSString stringWithFormat:@"Verify ID: %@", subjectId] withMessage:[NSString stringWithFormat:@"Verification status: %i - %@", status, [Utils faceVerificationStatusToNSString:status]]];
        [self presentViewController:alert animated:YES completion:nil];
    });

    NSLog(@"verifyTask done");
}

- (void)cancelOperationTask {
    NSLog(@"cancelOperationTask started");

    NRESULT_CHECK(NFaceVerificationClientCancel());

    NSLog(@"cancelOperationTask done");
}

- (void)clearDBTask {
    NSLog(@"clearDBTask started");
    [DBConnection initDB];
    dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertController *alert = [Utils createSimpleAlert:@"Clear DB" withMessage:@"Done."];
        [self presentViewController:alert animated:YES completion:nil];
    });

    NSLog(@"clearDBTask done");
}

- (IBAction)enrollClicked {
    NSLog(@"enrollClicked");

    [self showEnterIdDialog:@"Enroll" withCallback:^(NSString *subjectId) {
        if ([DBConnection subjectExists:subjectId]) {
            dispatch_async(dispatch_get_main_queue(), ^{
                UIAlertController *alert = [Utils createSimpleAlert:@"" withMessage:[NSString stringWithFormat:@"Subject %@ already exists", subjectId]];
                [self presentViewController:alert animated:YES completion:nil];
            });
        } else {
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                [self enrollTask:subjectId];
            });
        }
    }];
}

- (IBAction)verifyClicked {
    NSLog(@"verifyClicked");

    [self showSubjectSelectDialog];
}

- (IBAction)cancelClicked {
    NSLog(@"cancelClicked");

    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        [self cancelOperationTask];
    });
}

- (IBAction)clearDBClicked {
    NSLog(@"clearDBClicked");

    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        [self clearDBTask];
    });
}

- (IBAction)livenessModeClicked {
    NSLog(@"livenessModeClicked");

    [self showLivenessModeSelectDialog];
}

- (IBAction)settingsClicked:(id)sender {
}

@end
