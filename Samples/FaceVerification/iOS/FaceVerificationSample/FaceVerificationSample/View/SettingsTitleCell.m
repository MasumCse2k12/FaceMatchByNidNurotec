#import "SettingsTitleCell.h"
#import "Settings.h"

@implementation SettingsTitleCell
@synthesize settingLabel;

+(float)heightForCell{
    return 40;
}

-(void)setupWithSettingHandler:(SettingHandler*)handler {
    [settingLabel setText:handler.title];
}

@end
