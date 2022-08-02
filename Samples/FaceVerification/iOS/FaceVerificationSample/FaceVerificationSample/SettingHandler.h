#import <Foundation/Foundation.h>

@interface SettingHandler : NSObject

@property (nonatomic, retain) NSString *title;
@property (nonatomic, retain) NSString *key;
@property (nonatomic, retain) NSString *type;
@property (nonatomic) int minVal;
@property (nonatomic) int maxVal;
@property (nonatomic) int defaultVal;
@property (nonatomic) BOOL state;
@property (nonatomic) NSArray *options;

-(id)initWithTitle:(NSString*)titleArg key:(NSString*)keyArg type:(NSString*)typeArg;
-(void)setMin:(int)min andMax:(int)max;
-(void)setDefaultVal:(int)value;
-(void)setState:(BOOL)value;
-(void)setOptions:(NSArray *)array;

@end
