using HellBrick.Collections.Lists;

namespace HellBrick.Collections
{
	public static partial class VisitableList
	{
		public static VisitableList<Empty<TVisitor>, TVisitor> Empty<TVisitor>() => default;
	}
}
