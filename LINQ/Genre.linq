<Query Kind="Program">
  <Connection>
    <ID>ba174533-8b25-47e0-9779-48f0764eeaaf</ID>
    <Persist>true</Persist>
    <Driver Assembly="linq2db.LINQPad" PublicKeyToken="f19f8aed7feff67e">LinqToDB.LINQPad.LinqToDBDriver</Driver>
    <Server>.</Server>
    <Database>Chinook</Database>
    <DbVersion>12.00.4100</DbVersion>
    <CustomCxString>Data Source=.;Initial Catalog=Chinook;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=True;Persist Security Info=True</CustomCxString>
    <DisplayName>Chinook SQL Server - LINQ to DB</DisplayName>
    <NoPluralization>true</NoPluralization>
    <NoCapitalization>true</NoCapitalization>
    <DriverData>
      <providerName>SqlServer</providerName>
      <commandTimeout>0</commandTimeout>
      <providerVersion>SqlServer.2014</providerVersion>
    </DriverData>
  </Connection>
</Query>

// LINQPad
// Genre

void Main()
{
	(
		from x in Genre
		where
		    x.Name.CompareTo("r") >= 0
			|| x.Name.Contains("r")
			|| x.Name.StartsWith("r")
		orderby
			x.Name descending
		select new
		{
		    New1 = x.GenreId,
			New2 = x.Name.ToUpper()
		}
	)
	.Take(10)
	.Dump("Genre");
}

/*
// C# Expression

from x in Genre
where
    x.Name.CompareTo("r") >= 0
	|| x.Name.Contains("r")
	|| x.Name.StartsWith("r")
orderby
	x.Name descending
select new
{
    New1 = x.GenreId,
	New2 = x.Name.ToUpper()
}
*/