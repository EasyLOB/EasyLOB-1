<Query Kind="Statements" />

// LINQPad
// Comparison Int32

var query = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.AsQueryable();

(
	from x in query
	where
        x < 3
        || x.CompareTo(7) > 0
	orderby x descending
	select x
)
.Dump("Comparison Int32");

int value = 5;

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
.Dump("Comparison Int32");

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
.Dump("Comparison Int32");