#import "DBConnection.h"
#import "Utils.h"

sqlite3 *db;

@implementation DBConnection

+ (Boolean)execute:(NSString*)query {
    NSLog(@"%@", query);
    sqlite3_stmt *statement = nil;
    if (sqlite3_prepare_v2(db, [query UTF8String], -1, &statement, NULL) != SQLITE_OK) {
        NSLog(@"%s", sqlite3_errmsg(db));
        return false;
    }
    if (sqlite3_step(statement) != SQLITE_DONE) {
        NSLog(@"%s", sqlite3_errmsg(db));
    }
    sqlite3_finalize(statement);
    return true;
}

+ (Boolean)subjectExists:(NSString*)subject {
    sqlite3_stmt *selectStatement = nil;
    NSString *tableName = @"subjects";
    NSString *subjectId = @"subject_id";
    NSString *query = [NSString stringWithFormat:@"SELECT * FROM `%@` WHERE `%@` = \'%@\'", tableName, subjectId, subject];
    if (sqlite3_prepare_v2(db, [query UTF8String], -1, &selectStatement, NULL) != SQLITE_OK) {
        NSLog(@"%s", sqlite3_errmsg(db));
    }
    if (sqlite3_step(selectStatement) == SQLITE_ROW) {
        return true;
    }
    return false;
}

+ (NSMutableArray*)loadSubjects {
    NSLog(@"Load subjectIDs");
    
    NSMutableArray* response = [NSMutableArray array];
    NSString *tableName = @"subjects";
    NSString *subjectId = @"subject_id";
    sqlite3_stmt *statement = nil;
    NSString *query = [NSString stringWithFormat:@"SELECT `%@` FROM `%@`", subjectId, tableName];
    NSLog(@"%@", query);
    if (sqlite3_prepare_v2(db, [query UTF8String], -1, &statement, NULL) != SQLITE_OK) {
        NSLog(@"%s", sqlite3_errmsg(db));
    }
    while (sqlite3_step(statement) == SQLITE_ROW) {
        NSString *value = [NSString stringWithCString:(const char *)sqlite3_column_text(statement, 0)
                                             encoding:NSUTF8StringEncoding];
        [response addObject:value];
    }
    return response;
}

+ (NSData*)loadSubjectData:(NSString*)subject {
    NSLog(@"Load template");
    
    NSString *tableName = @"subjects";
    NSString *subjectId = @"subject_id";
    NSString *subjectTemplate = @"subject_template";
    sqlite3_stmt *statement = nil;
    NSString *query = [NSString stringWithFormat:@"SELECT `%@` FROM `%@` WHERE `%@` = \'%@\'", subjectTemplate, tableName, subjectId, subject];
    NSLog(@"%@", query);
    if (sqlite3_prepare_v2(db, [query UTF8String], -1, &statement, NULL) != SQLITE_OK) {
        NSLog(@"%s", sqlite3_errmsg(db));
    }
    if (sqlite3_step(statement) == SQLITE_ROW) {
        const void *blobBytes = sqlite3_column_blob(statement, 0);
        int blobBytesLength = sqlite3_column_bytes(statement, 0);
        NSData *blobData = [NSData dataWithBytes:blobBytes length:blobBytesLength];
        NSLog(@"%lu", (unsigned long)[blobData length]);
        return blobData;
    }
    return nil;
}

+ (void)save:(NSString*)subject withData:(void*)data ofLength:(int)length {
    NSLog(@"Save template");

    NSString *tableName = @"subjects";
    NSString *subjectId = @"subject_id";
    NSString *subjectTemplate = @"subject_template";
    NSString *query = [NSString stringWithFormat:@"INSERT INTO `%@` (`%@`, `%@`) VALUES (?, ?)", tableName, subjectId, subjectTemplate];
    sqlite3_stmt *statement = nil;
    if (sqlite3_prepare_v2(db, [query UTF8String], -1, &statement, NULL) == SQLITE_OK) {
        sqlite3_bind_text(statement, 1, [subject UTF8String], -1, SQLITE_TRANSIENT);
        sqlite3_bind_blob(statement, 2, data, length, SQLITE_STATIC);
        NSLog(@"%d", length);
    }
    if (sqlite3_step(statement) != SQLITE_DONE) {
        NSLog(@"%s", sqlite3_errmsg(db));
    }
    sqlite3_finalize(statement);
    
}

+ (Boolean)initDB {
    NSLog(@"Initialize database");

    NSString *appSupportDir = [Utils getAppSupportDir];
    NSString *dbFilename = @"database.db";
    NSString *dbPath = [appSupportDir stringByAppendingPathComponent:dbFilename];
    
    NSString *tableName = @"subjects";
    NSString *subjectId = @"subject_id";
    NSString *subjectTemplate = @"subject_template";
    
    NSString *sqlCreateTable = [NSString stringWithFormat:@"CREATE TABLE %@ (%@ TEXT PRIMARY KEY, %@ BLOB)",
                                tableName, subjectId, subjectTemplate];
    
    // removes database on every init
    [Utils removeFileAtPath:dbPath];
    if (sqlite3_open([dbPath UTF8String], &db) != SQLITE_OK) {
        return false;
    } else if (![self execute:sqlCreateTable]) {
        return false;
    }
    return true;
}

@end
