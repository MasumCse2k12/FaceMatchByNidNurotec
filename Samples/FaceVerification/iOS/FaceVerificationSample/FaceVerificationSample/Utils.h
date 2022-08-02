#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <NFaceVerificationClient/NFaceVerificationClient.h>
#import <sqlite3.h>

#define NRESULT_CHECK(func)\
    {\
        NResult result = (func);\
        if (result < 0){\
            NChar* message;\
            NInt msgLength;\
NSLog(@"aaa");\
            NFaceVerificationClientGetLastErrorMessage(&message, &msgLength);\
            NSString *exceptionString = [[NSString alloc] initWithUTF8String:message];\
            @throw [NSException exceptionWithName:@"NException" reason:exceptionString userInfo:nil];\
        }\
    };

@interface Utils : NSObject

+ (NSString* _Nonnull)getAppSupportDir;
+ (void)removeFileAtPath:( NSString* _Nonnull )path;
+ (UIAlertController* _Nonnull)createSimpleAlert:(NSString* _Nullable)title withMessage:(NSString* _Nonnull)message;
+ (NSString* _Nonnull)faceVerificationStatusToNSString:(NfvcStatus)status;
+ (NSData*) sendPostRequest: (NSString* _Nonnull)url withData:(void*)data withLength:(size_t)bytes;

@end
