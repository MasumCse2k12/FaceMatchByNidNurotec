#import "SettingHandler.h"

@implementation SettingHandler
@synthesize type, title, key, minVal, maxVal, state, defaultVal, options;

-(id)initWithTitle:(NSString*)titleArg key:(NSString*)keyArg type:(NSString*)typeArg {
    title = titleArg;
    key = keyArg;
    type = typeArg;
    minVal = 0;
    maxVal = 100;
    defaultVal = 50;
    state = false;
    return self;
}

-(void)setMin:(int)min andMax:(int)max {
    minVal = min;
    maxVal = max;
}

-(void)setDefaultVal:(int)value {
    defaultVal = value;
}

-(void)setState:(BOOL)value {
    state = value;
}

-(void)setOptions:(NSArray *)array {
    options = array;
    defaultVal = 0;
}

@end
