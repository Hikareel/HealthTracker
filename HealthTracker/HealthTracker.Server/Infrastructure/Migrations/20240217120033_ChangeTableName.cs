using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentTable_CommentTable_ParentCommentId",
                table: "CommentTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTable_PostTable_PostId",
                table: "CommentTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentTable_UserTable_UserId",
                table: "CommentTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseTypeTable_ExerciseTypeId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseWorkout_WorkoutTable_WorkoutsId",
                table: "ExerciseWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipTable_StatusTable_StatusId",
                table: "FriendshipTable");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipTable_UserTable_User1Id",
                table: "FriendshipTable");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipTable_UserTable_User2Id",
                table: "FriendshipTable");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalTable_GoalTypeTable_GoalTypeId",
                table: "GoalTable");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalTable_UserTable_UserId",
                table: "GoalTable");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthMeasurementTable_MeasurementTypeTable_MeasurementType~",
                table: "HealthMeasurementTable");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthMeasurementTable_UserTable_UserId",
                table: "HealthMeasurementTable");

            migrationBuilder.DropForeignKey(
                name: "FK_MealUserTable_MealTable_MealId",
                table: "MealUserTable");

            migrationBuilder.DropForeignKey(
                name: "FK_MealUserTable_MealTypeTable_MealTypeId",
                table: "MealUserTable");

            migrationBuilder.DropForeignKey(
                name: "FK_MealUserTable_UserTable_UserId",
                table: "MealUserTable");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTable_StatusTable_StatusId",
                table: "NotificationTable");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTable_UserTable_UserId",
                table: "NotificationTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTable_UserTable_UserId",
                table: "PostTable");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTable_UserTable_UserId",
                table: "WorkoutTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutTable",
                table: "WorkoutTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTable",
                table: "UserTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusTable",
                table: "StatusTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTable",
                table: "PostTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTable",
                table: "NotificationTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementTypeTable",
                table: "MeasurementTypeTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealUserTable",
                table: "MealUserTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealTypeTable",
                table: "MealTypeTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealTable",
                table: "MealTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthMeasurementTable",
                table: "HealthMeasurementTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalTypeTable",
                table: "GoalTypeTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalTable",
                table: "GoalTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendshipTable",
                table: "FriendshipTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseTypeTable",
                table: "ExerciseTypeTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentTable",
                table: "CommentTable");

            migrationBuilder.RenameTable(
                name: "WorkoutTable",
                newName: "Workout");

            migrationBuilder.RenameTable(
                name: "UserTable",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "StatusTable",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "PostTable",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "NotificationTable",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "MeasurementTypeTable",
                newName: "MeasurementType");

            migrationBuilder.RenameTable(
                name: "MealUserTable",
                newName: "MealUser");

            migrationBuilder.RenameTable(
                name: "MealTypeTable",
                newName: "MealType");

            migrationBuilder.RenameTable(
                name: "MealTable",
                newName: "Meal");

            migrationBuilder.RenameTable(
                name: "HealthMeasurementTable",
                newName: "HealthMeasurement");

            migrationBuilder.RenameTable(
                name: "GoalTypeTable",
                newName: "GoalType");

            migrationBuilder.RenameTable(
                name: "GoalTable",
                newName: "Goal");

            migrationBuilder.RenameTable(
                name: "FriendshipTable",
                newName: "Friendship");

            migrationBuilder.RenameTable(
                name: "ExerciseTypeTable",
                newName: "ExerciseType");

            migrationBuilder.RenameTable(
                name: "CommentTable",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutTable_UserId",
                table: "Workout",
                newName: "IX_Workout_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTable_UserId",
                table: "Post",
                newName: "IX_Post_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTable_UserId",
                table: "Notification",
                newName: "IX_Notification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTable_StatusId",
                table: "Notification",
                newName: "IX_Notification_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_MealUserTable_UserId",
                table: "MealUser",
                newName: "IX_MealUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MealUserTable_MealTypeId",
                table: "MealUser",
                newName: "IX_MealUser_MealTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MealUserTable_MealId",
                table: "MealUser",
                newName: "IX_MealUser_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthMeasurementTable_UserId",
                table: "HealthMeasurement",
                newName: "IX_HealthMeasurement_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthMeasurementTable_MeasurementTypeId",
                table: "HealthMeasurement",
                newName: "IX_HealthMeasurement_MeasurementTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalTable_UserId",
                table: "Goal",
                newName: "IX_Goal_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalTable_GoalTypeId",
                table: "Goal",
                newName: "IX_Goal_GoalTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendshipTable_User2Id",
                table: "Friendship",
                newName: "IX_Friendship_User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_FriendshipTable_User1Id",
                table: "Friendship",
                newName: "IX_Friendship_User1Id");

            migrationBuilder.RenameIndex(
                name: "IX_FriendshipTable_StatusId",
                table: "Friendship",
                newName: "IX_Friendship_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentTable_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentTable_PostId",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentTable_ParentCommentId",
                table: "Comment",
                newName: "IX_Comment_ParentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementType",
                table: "MeasurementType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealUser",
                table: "MealUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealType",
                table: "MealType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meal",
                table: "Meal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthMeasurement",
                table: "HealthMeasurement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalType",
                table: "GoalType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseType",
                table: "ExerciseType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseType_ExerciseTypeId",
                table: "Exercise",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseWorkout_Workout_WorkoutsId",
                table: "ExerciseWorkout",
                column: "WorkoutsId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Status_StatusId",
                table: "Friendship",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_User1Id",
                table: "Friendship",
                column: "User1Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_User2Id",
                table: "Friendship",
                column: "User2Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_GoalType_GoalTypeId",
                table: "Goal",
                column: "GoalTypeId",
                principalTable: "GoalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_User_UserId",
                table: "Goal",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthMeasurement_MeasurementType_MeasurementTypeId",
                table: "HealthMeasurement",
                column: "MeasurementTypeId",
                principalTable: "MeasurementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthMeasurement_User_UserId",
                table: "HealthMeasurement",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealUser_MealType_MealTypeId",
                table: "MealUser",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealUser_Meal_MealId",
                table: "MealUser",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealUser_User_UserId",
                table: "MealUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Status_StatusId",
                table: "Notification",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_User_UserId",
                table: "Workout",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExerciseType_ExerciseTypeId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseWorkout_Workout_WorkoutsId",
                table: "ExerciseWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Status_StatusId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_User1Id",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_User2Id",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_GoalType_GoalTypeId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_User_UserId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthMeasurement_MeasurementType_MeasurementTypeId",
                table: "HealthMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthMeasurement_User_UserId",
                table: "HealthMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_MealUser_MealType_MealTypeId",
                table: "MealUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MealUser_Meal_MealId",
                table: "MealUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MealUser_User_UserId",
                table: "MealUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Status_StatusId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_User_UserId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementType",
                table: "MeasurementType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealUser",
                table: "MealUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealType",
                table: "MealType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meal",
                table: "Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthMeasurement",
                table: "HealthMeasurement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalType",
                table: "GoalType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseType",
                table: "ExerciseType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "WorkoutTable");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserTable");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "StatusTable");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "PostTable");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "NotificationTable");

            migrationBuilder.RenameTable(
                name: "MeasurementType",
                newName: "MeasurementTypeTable");

            migrationBuilder.RenameTable(
                name: "MealUser",
                newName: "MealUserTable");

            migrationBuilder.RenameTable(
                name: "MealType",
                newName: "MealTypeTable");

            migrationBuilder.RenameTable(
                name: "Meal",
                newName: "MealTable");

            migrationBuilder.RenameTable(
                name: "HealthMeasurement",
                newName: "HealthMeasurementTable");

            migrationBuilder.RenameTable(
                name: "GoalType",
                newName: "GoalTypeTable");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "GoalTable");

            migrationBuilder.RenameTable(
                name: "Friendship",
                newName: "FriendshipTable");

            migrationBuilder.RenameTable(
                name: "ExerciseType",
                newName: "ExerciseTypeTable");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "CommentTable");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_UserId",
                table: "WorkoutTable",
                newName: "IX_WorkoutTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UserId",
                table: "PostTable",
                newName: "IX_PostTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_UserId",
                table: "NotificationTable",
                newName: "IX_NotificationTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_StatusId",
                table: "NotificationTable",
                newName: "IX_NotificationTable_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_MealUser_UserId",
                table: "MealUserTable",
                newName: "IX_MealUserTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MealUser_MealTypeId",
                table: "MealUserTable",
                newName: "IX_MealUserTable_MealTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MealUser_MealId",
                table: "MealUserTable",
                newName: "IX_MealUserTable_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthMeasurement_UserId",
                table: "HealthMeasurementTable",
                newName: "IX_HealthMeasurementTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthMeasurement_MeasurementTypeId",
                table: "HealthMeasurementTable",
                newName: "IX_HealthMeasurementTable_MeasurementTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_UserId",
                table: "GoalTable",
                newName: "IX_GoalTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_GoalTypeId",
                table: "GoalTable",
                newName: "IX_GoalTable_GoalTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_User2Id",
                table: "FriendshipTable",
                newName: "IX_FriendshipTable_User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_User1Id",
                table: "FriendshipTable",
                newName: "IX_FriendshipTable_User1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_StatusId",
                table: "FriendshipTable",
                newName: "IX_FriendshipTable_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "CommentTable",
                newName: "IX_CommentTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "CommentTable",
                newName: "IX_CommentTable_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ParentCommentId",
                table: "CommentTable",
                newName: "IX_CommentTable_ParentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutTable",
                table: "WorkoutTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTable",
                table: "UserTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusTable",
                table: "StatusTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTable",
                table: "PostTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTable",
                table: "NotificationTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementTypeTable",
                table: "MeasurementTypeTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealUserTable",
                table: "MealUserTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealTypeTable",
                table: "MealTypeTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealTable",
                table: "MealTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthMeasurementTable",
                table: "HealthMeasurementTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalTypeTable",
                table: "GoalTypeTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalTable",
                table: "GoalTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendshipTable",
                table: "FriendshipTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseTypeTable",
                table: "ExerciseTypeTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentTable",
                table: "CommentTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTable_CommentTable_ParentCommentId",
                table: "CommentTable",
                column: "ParentCommentId",
                principalTable: "CommentTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTable_PostTable_PostId",
                table: "CommentTable",
                column: "PostId",
                principalTable: "PostTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTable_UserTable_UserId",
                table: "CommentTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExerciseTypeTable_ExerciseTypeId",
                table: "Exercise",
                column: "ExerciseTypeId",
                principalTable: "ExerciseTypeTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseWorkout_WorkoutTable_WorkoutsId",
                table: "ExerciseWorkout",
                column: "WorkoutsId",
                principalTable: "WorkoutTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipTable_StatusTable_StatusId",
                table: "FriendshipTable",
                column: "StatusId",
                principalTable: "StatusTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipTable_UserTable_User1Id",
                table: "FriendshipTable",
                column: "User1Id",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipTable_UserTable_User2Id",
                table: "FriendshipTable",
                column: "User2Id",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalTable_GoalTypeTable_GoalTypeId",
                table: "GoalTable",
                column: "GoalTypeId",
                principalTable: "GoalTypeTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalTable_UserTable_UserId",
                table: "GoalTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthMeasurementTable_MeasurementTypeTable_MeasurementType~",
                table: "HealthMeasurementTable",
                column: "MeasurementTypeId",
                principalTable: "MeasurementTypeTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthMeasurementTable_UserTable_UserId",
                table: "HealthMeasurementTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealUserTable_MealTable_MealId",
                table: "MealUserTable",
                column: "MealId",
                principalTable: "MealTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealUserTable_MealTypeTable_MealTypeId",
                table: "MealUserTable",
                column: "MealTypeId",
                principalTable: "MealTypeTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealUserTable_UserTable_UserId",
                table: "MealUserTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTable_StatusTable_StatusId",
                table: "NotificationTable",
                column: "StatusId",
                principalTable: "StatusTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTable_UserTable_UserId",
                table: "NotificationTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTable_UserTable_UserId",
                table: "PostTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTable_UserTable_UserId",
                table: "WorkoutTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
