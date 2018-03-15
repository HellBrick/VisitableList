namespace HellBrick.Collections
{
	public interface IVisitableList<TVisitor> : IVisitable<TVisitor>
	{
		int Count { get; }
	}
}
