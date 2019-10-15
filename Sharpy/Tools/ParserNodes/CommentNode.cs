namespace ASmarty.Tools.ParserNodes
{
    internal class CommentNode : IParserNode
    {
        public int Id { get; private set; }

        public CommentNode(int id)
        {
            Id = id;
        }
    }
}
