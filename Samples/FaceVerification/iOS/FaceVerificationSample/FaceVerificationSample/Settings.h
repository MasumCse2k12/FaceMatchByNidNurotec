#import <Foundation/Foundation.h>
#import "Utils.h"
#import "SettingHandler.h"

@interface Settings : NSObject
+ (NSString*)getSettingString:(NSString*)setting;
+ (void)updateSetting:(SettingHandler*)handler;
+ (NSArray*)getAllSettingHandlers;
+ (void)initSettings;

@end
