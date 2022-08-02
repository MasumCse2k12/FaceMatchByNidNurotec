#import "SettingsPickerCell.h"
#import "Settings.h"

@implementation SettingsPickerCell
@synthesize settingLabel, settingHandler, button;

+(float)heightForCell{
    return 70;
}

-(void)setupWithSettingHandler:(SettingHandler*)handler {
    [settingLabel setText:handler.title];
    settingHandler = handler;
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    [button setTitle:[settingHandler.options objectAtIndex:[userDefaults integerForKey:settingHandler.key]] forState:UIControlStateNormal];
}
- (IBAction)openPicker:(id)sender {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    UIViewController *viewController = [[[[UIApplication sharedApplication] delegate] window] rootViewController];
    if ( viewController.presentedViewController && !viewController.presentedViewController.isBeingDismissed ) {
        viewController = viewController.presentedViewController;
    }
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:settingHandler.title message:nil preferredStyle:UIAlertControllerStyleAlert];
    for (int i = 0; i < settingHandler.options.count; i++) {
        UIAlertAction *action = [UIAlertAction actionWithTitle:[settingHandler.options objectAtIndex:i] style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
            [alert dismissViewControllerAnimated:YES completion:nil];
            [userDefaults setInteger:i forKey:settingHandler.key];
            [button setTitle:[settingHandler.options objectAtIndex:[userDefaults integerForKey:settingHandler.key]] forState:UIControlStateNormal];
            [Settings updateSetting:settingHandler];
        }];
        [alert addAction:action];
    }
    UIAlertAction *cancel = [UIAlertAction actionWithTitle:@"Cancel" style:UIAlertActionStyleCancel handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
    }];
    [alert addAction:cancel];
    NSLayoutConstraint *constraint = [NSLayoutConstraint
                                      constraintWithItem:alert.view
                                      attribute:NSLayoutAttributeHeight
                                      relatedBy:NSLayoutRelationLessThanOrEqual
                                      toItem:nil
                                      attribute:NSLayoutAttributeNotAnAttribute
                                      multiplier:1
                                      constant:viewController.view.frame.size.height*2.0f];
    
    [alert.view addConstraint:constraint];
    [viewController presentViewController:alert animated:YES completion:^{}];
}

//- (IBAction)updateValue:(id)sender {
//    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
//    [userDefaults setBool:[uiSwitch isOn] forKey:settingHandler.key];
//}

@end
