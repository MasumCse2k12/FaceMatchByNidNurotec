#import "SettingsSliderCell.h"
#import "Settings.h"

@implementation SettingsSliderCell
@synthesize settingLabel, minVal, maxVal, settingHandler, slider, currentVal;

+(float)heightForCell{
    return 90;
}

-(void)setupWithSettingHandler:(SettingHandler*)handler {
    [settingLabel setText:handler.title];
    settingHandler = handler;
    [minVal setText:[NSString stringWithFormat:@"%d", handler.minVal]];
    [slider setMinimumValue:handler.minVal];
    maxVal.textAlignment = NSTextAlignmentRight;
    [maxVal setText:[NSString stringWithFormat:@"%d", handler.maxVal]];
    [slider setMaximumValue:handler.maxVal];
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    slider.value = [userDefaults integerForKey:settingHandler.key];
    [currentVal setText:[NSString stringWithFormat:@"%d", (int)slider.value]];
}
- (IBAction)updateValue:(id)sender {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    int value = (int)slider.value;
    [userDefaults setInteger:value forKey:settingHandler.key];
    slider.value = value;
    [currentVal setText:[NSString stringWithFormat:@"%d", (int)slider.value]];
    [Settings updateSetting:settingHandler];
}

@end
