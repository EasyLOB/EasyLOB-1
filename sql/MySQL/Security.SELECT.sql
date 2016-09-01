/*
USE EasyLOBSecurity;
*/

SELECT
    AspNetUsers.UserName,
    AspNetUsers.Email,
    AspNetRoles.Name,
    AspNetActivity.Name,
    AspNetActivityRoles.Operations
FROM
    AspNetUsers
    LEFT JOIN AspNetUserRoles ON
        AspNetUserRoles.UserId = AspNetUsers.Id
    LEFT JOIN AspNetRoles ON
        AspNetRoles.Id = AspNetUserRoles.RoleId
    LEFT JOIN AspNetActivityRoles ON
        AspNetActivityRoles.RoleId = AspNetRoles.Id
    LEFT JOIN AspNetActivity ON
        AspNetActivity.Id = AspNetActivityRoles.ActivityId
ORDER BY
    1,3,4;
