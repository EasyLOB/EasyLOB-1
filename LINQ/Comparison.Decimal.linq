<Query Kind="Statements" />

// LINQPad
// Comparison Decimal

var query = new decimal[] { 1.1M, 2.2M, 3.3M, 4.4M, 5.5M, 6.6M, 7.7M, 8.8M, 9.9M }.AsQueryable();

(
	from x in query
	where
        x < 3
        || x.CompareTo(7) > 0
	orderby x descending
	select x
)
.Dump("Comparison Decimal");

decimal value = 5M;

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
.Dump("Comparison Decimal");

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
.Dump("Comparison Decimal");