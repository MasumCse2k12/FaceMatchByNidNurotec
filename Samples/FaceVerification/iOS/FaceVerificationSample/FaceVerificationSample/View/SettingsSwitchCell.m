#import "SettingsSwitchCell.h"
#import "Settings.h"

@implementation SettingsSwitchCell
@synthesize settingLabel, settingHandler, uiSwitch;

+(float)heightForCell{
    return 40;
}

-(void)setupWithSettingHandler:(SettingHandler*)handler {
    [settingLabel setText:handler.title];
    settingHandler = handler;
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    [uiSwitch setOn:[userDefaults boolForKey:settingHandler.key]];
}

- (IBAction)updateValue:(id)sender {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    [userDefaults setBool:[uiSwitch isOn] forKey:settingHandler.key];
    [Settings updateSetting:settingHandler];
}

@end
