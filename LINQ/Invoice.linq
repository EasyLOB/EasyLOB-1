<Query Kind="Program">
  <Connection>
    <ID>c5f00771-2d74-44f1-a7b2-92975f053245</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>Chinook</Database>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAq04HmwEQzkOqoCKSunXsYAAAAAACAAAAAAAQZgAAAAEAACAAAAA7TfhnfSq067oD2J799qv+2M2P4BiozzfTRkpHlSpiogAAAAAOgAAAAAIAACAAAAD2/6tqipIcBd7Mx86AuL7mNrmDY2VBk0GIW8de0jeixxAAAABGYBX4rRnLrWnW/9xejohaQAAAAHdVlxNCuJduT9J1q+W52KQm6zN9HC9xzxSmy5XXZoGG8K6VSQ479G8AglBxD+hm4EqD44zQLM7173BIpuCze3I=</Password>
    <NoPluralization>true</NoPluralization>
  </Connection>
</Query>

// LINQPad
// Invoice

void Main()
{
    /*
	DateTime date = new DateTime(2009, 1, 1);
	double total = 1;
	
	(
		from
			x in Invoice
		where
		    // InvoiceDate
		    DateTime.Compare(x.InvoiceDate, date) > 0
		    || x.InvoiceDate.CompareTo(date) == 0
			// Total
		    || x.Total.CompareTo(total) > 0
		orderby
			x.InvoiceDate ascending
		select new
		{
			New1 = x.InvoiceDate,
			New2 = x.Total
		}
	)
	.Take(10)
	.Dump("Invoice");
	 */
	string value = "2016";
	
	(
		from
			x in Invoice
		where
		    // InvoiceDate "Jul  4 2016 12:00AM"
		    x.InvoiceDate.ToString().Contains(value)
			// Total "1.23"
		    || x.Total.ToString().Contains(value)
		orderby
			x.InvoiceDate ascending
		select new
		{
			New1 = x.InvoiceDate.ToString(),
			New2 = x.Total.ToString()
		}
	)
	.Take(10)
	.Dump("Invoice");	
}