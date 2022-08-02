#import <Foundation/Foundation.h>
#import <sqlite3.h>

@interface DBConnection : NSObject

+ (Boolean)initDB;
+ (NSMutableArray*)loadSubjects;
+ (Boolean)subjectExists:(NSString*)subject;
+ (NSData*)loadSubjectData:(NSString*)subject;
+ (void)save:(NSString*)subject withData:(void*)data ofLength:(int)length;
+ (Boolean)execute:(NSString*)query;

@end
