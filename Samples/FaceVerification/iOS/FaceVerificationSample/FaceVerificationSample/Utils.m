#import "Utils.h"
#import "Settings.h"

@implementation Utils

+ (NSString* _Nonnull)getAppSupportDir {
    NSString *appSupportDir = [NSSearchPathForDirectoriesInDomains(NSApplicationSupportDirectory, NSUserDomainMask, YES) lastObject];
    NSFileManager *manager = [NSFileManager defaultManager];
    if(![manager fileExistsAtPath:appSupportDir]) {
        NSLog(@"App support dir does not exist, creating one: %@", appSupportDir);
        NSError *error;
        BOOL ret = [manager createDirectoryAtPath:appSupportDir withIntermediateDirectories:NO attributes:nil error:&error];
        if(!ret) {
            @throw [NSException exceptionWithName:@"AppSupportDirCreationException" reason:[NSString stringWithFormat:@"Error: %@", error] userInfo:nil];
        }
    }
    return appSupportDir;
}

+ (void)removeFileAtPath:( NSString* _Nonnull )path {
    NSFileManager *manager = [NSFileManager defaultManager];
    if([manager fileExistsAtPath:path]) {
        NSError *error;
        BOOL ret = [manager removeItemAtPath:path error:&error];
        if(!ret) {
            @throw [NSException exceptionWithName:@"RemoveFileException" reason:[NSString stringWithFormat:@"Failed to remove %@: %@", path, error] userInfo:nil];
        }
    } else {
        NSLog(@"Nothing to remove: %@", path);
    }
}

+ (UIAlertController* _Nonnull)createSimpleAlert:(NSString* _Nullable)title withMessage:(NSString* _Nonnull)message {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:title message:message preferredStyle:UIAlertControllerStyleAlert];

    UIAlertAction *ok = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
    }];

    [alert addAction:ok];

    return alert;
}

+ (NSString* _Nonnull)faceVerificationStatusToNSString:(NfvcStatus)status {
    if (status == nfvcsNone) {
        return @"None";
    } else if (status == nfvcsSuccess) {
        return @"Success";
    } else if (status == nfvcsTimeout) {
        return @"Timeout";
    } else if (status == nfvcsCanceled) {
        return @"Cancelled";
    } else if (status == nfvcsBadQuality) {
        return @"Bad quality";
    } else if (status == nfvcsMatchNotFound) {
        return @"Match not found";
    } else if (status == nfvcsCameraNotFound) {
        return @"Camera not found";
    } else if (status == nfvcsFaceNotFound) {
        return @"Face not found";
    } else if (status == nfvcsLivenessCheckFailed) {
        return @"Liveness check failed";
    }
    return @"";
}

+ (NSData*) sendPostRequest: (NSString* _Nonnull)url withData:(void*)data withLength:(size_t)bytes {
    NSString *requestURL = [NSString stringWithFormat:@"%@validate", [Settings getSettingString:@"api"]];
    NSString *token = [Settings getSettingString:@"token"];

    NSData *nsData = [[NSData alloc] initWithBytes:data length:bytes];
    NSMutableURLRequest *request = [[NSMutableURLRequest alloc] init];
    [request setURL:[NSURL URLWithString:requestURL]];
    [request setHTTPMethod:@"POST"];
    [request setValue:token forHTTPHeaderField:@"X-Auth-token"];
    [request setValue:@"application/octet-stream" forHTTPHeaderField:@"Content-Type"];
    [request setHTTPBody:nsData];
    NSData __block *responseData;
    Boolean __block processed = false;
    [[[NSURLSession sharedSession] dataTaskWithRequest:request completionHandler:^(NSData * _Nullable data, NSURLResponse * _Nullable response, NSError * _Nullable error) {
        NSLog(@"%@", response);
        NSLog(@"%ld", [error code]);
        responseData = data;
        processed = true;
    }] resume];
    
    while (!processed) {
        [NSThread sleepForTimeInterval:0];
    }
    return responseData;
}

@end
