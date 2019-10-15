namespace ASmarty.Tools.ParserNodes
{
    public class HtmlNode : IParserNode
    {
        public int Id { get; private set; }
        public string Content { get; private set; }

        public HtmlNode(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
