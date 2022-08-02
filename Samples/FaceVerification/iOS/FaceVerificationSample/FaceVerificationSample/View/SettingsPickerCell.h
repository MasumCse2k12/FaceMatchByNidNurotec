#import <UIKit/UIKit.h>
#import "SettingHandler.h"

@interface SettingsPickerCell : UICollectionViewCell {
    
}

@property (weak, nonatomic) IBOutlet UILabel *settingLabel;
@property (nonatomic, retain) SettingHandler *settingHandler;
@property (weak, nonatomic) IBOutlet UIButton *button;

+(float)heightForCell;
-(void)setupWithSettingHandler:(SettingHandler*)handler;

@end
