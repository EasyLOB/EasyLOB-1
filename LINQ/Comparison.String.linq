<Query Kind="Statements" />

// LINQPad
// Comparison String

var query = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" }.AsQueryable();

(
	from x in query
	where
	    x == "1"
        || x.CompareTo("5") > 0
        || x.Contains("3")
        || x.Equals("2")
        || x.EndsWith("5")
        || x.StartsWith("4")
	orderby x descending
	select x
)
.Dump("Comparison String");

string value = "5";

(
	from x in query
	where
		// contains
	    x.Contains(value)
		// endswith
        || x.EndsWith(value)
		// equal
        || x.Equals(value)
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
        || x.StartsWith(value)
	orderby x descending
	select x
)
.Dump("Comparison String");

(
	from x in query
	where
		// contains
	    x.ToString().Contains(value)
		// endswith
        || x.ToString().EndsWith(value)
		// equal
        || x.ToString().Equals(value)
		// greaterthan
        || x.ToString().CompareTo(value) > 0
		// greaterthanorequal
        || x.ToString().CompareTo(value) >= 0
		// lessthan
        || x.ToString().CompareTo(value) < 0
		// lessthanorequal
        || x.ToString().CompareTo(value) <= 0
		// notequal
        || !x.ToString().Equals(value)
		// startswith
        || x.ToString().StartsWith(value)
	orderby x descending
	select x
)
.Dump("Comparison String");