#import <UIKit/UIKit.h>
#import "SettingHandler.h"

@interface SettingsSliderCell : UICollectionViewCell {
    
}

@property (weak, nonatomic) IBOutlet UILabel *settingLabel;
@property (weak, nonatomic) IBOutlet UISlider *slider;
@property (weak, nonatomic) IBOutlet UILabel *minVal;
@property (weak, nonatomic) IBOutlet UILabel *maxVal;
@property (weak, nonatomic) IBOutlet UILabel *currentVal;
@property (nonatomic, retain) SettingHandler *settingHandler;

+(float)heightForCell;
-(void)setupWithSettingHandler:(SettingHandler*)handler;

@end
