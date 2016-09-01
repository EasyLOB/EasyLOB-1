<Query Kind="Expression">
  <Connection>
    <ID>97ba2b70-3268-4bd0-bfb8-56692e3ffc0a</ID>
    <Persist>true</Persist>
    <Driver Assembly="linq2db.LINQPad" PublicKeyToken="f19f8aed7feff67e">LinqToDB.LINQPad.LinqToDBDriver</Driver>
    <Server>.</Server>
    <Database>EasyLOBSecurity</Database>
    <DbVersion>12.00.4100</DbVersion>
    <CustomCxString>Data Source=.;Initial Catalog=EasyLOBSecurity;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=True;Persist Security Info=True</CustomCxString>
    <DisplayName>EasyLOBSecurity - LINQ to DB</DisplayName>
    <DriverData>
      <providerName>SqlServer</providerName>
      <commandTimeout>0</commandTimeout>
      <providerVersion>SqlServer.2014</providerVersion>
    </DriverData>
  </Connection>
</Query>

// LINQPad
// EasyLOBSecurity

from
    ActivityRole in repositoryActivityRole.Query
from
    Activity in repositoryActivity.Query
from
    UserRole in repositoryUserRole.Query
where
    ActivityRole.ActivityId == Activity.Id
    && Activity.Name == activity
    && ActivityRole.RoleId == UserRole.RoleId
    && UserRole.UserId == IdentityHelper.UserId
select
    ActivityRole
