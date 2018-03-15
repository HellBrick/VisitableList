using HellBrick.Collections.Lists;

namespace HellBrick.Collections
{
	public static partial class VisitableList
	{
		public static VisitableList<SingleItem<TVisitor, TItem>, TVisitor> Add<TVisitor, TItem>
		(
			this VisitableList<Empty<TVisitor>, TVisitor> emptyList,
			TItem item
		)
			where TItem : IVisitable<TVisitor>
			=> new VisitableList<SingleItem<TVisitor, TItem>, TVisitor>( new SingleItem<TVisitor, TItem>( item ) );

		public static VisitableList<Concat<TVisitor, THead, SingleItem<TVisitor, TItem>>, TVisitor> Add<TVisitor, THead, TItem>
		(
			this VisitableList<THead, TVisitor> list,
			TItem item
		)
			where THead : struct, IVisitableList<TVisitor>
			where TItem : IVisitable<TVisitor>
			=> list.AddList( new VisitableList<SingleItem<TVisitor, TItem>, TVisitor>( new SingleItem<TVisitor, TItem>( item ) ) );

		public static VisitableList<Concat<TVisitor, THead, TTail>, TVisitor> AddList<TVisitor, THead, TTail>
		(
			this VisitableList<THead, TVisitor> firstList,
			VisitableList<TTail, TVisitor> secondList
		)
			where THead : struct, IVisitableList<TVisitor>
			where TTail : struct, IVisitableList<TVisitor>
			=> new VisitableList<Concat<TVisitor, THead, TTail>, TVisitor>( new Concat<TVisitor, THead, TTail>( firstList.List, secondList.List ) );
	}
}
