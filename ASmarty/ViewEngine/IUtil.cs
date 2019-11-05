using System;

namespace ASmarty.ViewEngine
{
    public interface IUtil
    {
        string MapPath(String path);

        string Content(String path);

        string HtmlEncode(String content);
    }
}
