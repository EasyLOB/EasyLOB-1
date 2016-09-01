<Query Kind="Statements" />

// LINQPad
// Comparison Double

var query = new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 }.AsQueryable();
double total33 = 3.3;
double total77 = 7.7;

(
	from x in query
	where
        x < 3
        || x.CompareTo(7) > 0
	orderby x descending
	select x
)
.Dump("Comparison Double");

double value = 5.5;

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
.Dump("Comparison Double");

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
.Dump("Comparison Double");