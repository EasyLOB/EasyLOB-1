<Query Kind="Statements" />

// LINQPad
// Comparison DateTime

var query = new DateTime[]
{
    new DateTime(2001, 1, 11),
    new DateTime(2002, 2, 12),
    new DateTime(2003, 3, 13),
    new DateTime(2004, 4, 14),
    new DateTime(2005, 5, 15),
}
.AsQueryable();

DateTime date = new DateTime(2003, 3, 13);

(
	from x in query
	where
        x.CompareTo(date) < 0
        || DateTime.Compare(x, date) > 0
	orderby x ascending
	select String.Format("{0:dd|MM|yyyy}", x)
)
.Dump("Comparison DateTime");

(
	from x in query
	where
        x.Year == date.Year
	orderby x ascending
	select String.Format("{0:yyyy}", x)
)
.Dump("Comparison DateTime");

DateTime value = new DateTime(2003, 3, 13);

(
	from x in query
	where
		// contains
	    //x.Contains(value)
		// endswith
        //|| x.EndsWith(value)
		// equal
        x.Equals(value)
		// greaterthan
        || x.CompareTo(value) > 0
		// greaterthanorequal
        || x.CompareTo(value) >= 0
		// lessthan
        || x.CompareTo(value) < 0
		// lessthanorequal
        || x.CompareTo(value) <= 0
		// notequal
        || !x.Equals(value)
		// startswith
        //|| x.StartsWith(value)
	orderby x descending
	select x
)
.Dump("Comparison DateTime");

(
	from x in query
	where
		// contains
	    x.ToString().Contains(value.ToString())
		// endswith
        || x.ToString().EndsWith(value.ToString())
		// equal
        || x.ToString().Equals(value.ToString())
		// greaterthan
        || x.ToString().CompareTo(value.ToString()) > 0
		// greaterthanorequal
        || x.ToString().CompareTo(value.ToString()) >= 0
		// lessthan
        || x.ToString().CompareTo(value.ToString()) < 0
		// lessthanorequal
        || x.ToString().CompareTo(value.ToString()) <= 0
		// notequal
        || !x.ToString().Equals(value.ToString())
		// startswith
        || x.ToString().StartsWith(value.ToString())
	orderby x descending
	select x
)
.Dump("Comparison DateTime");