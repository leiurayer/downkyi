namespace DownKyi.Core.FileName
{
    public enum FileNamePart
    {
        // Video
        ORDER = 1,
        SECTION,
        MAIN_TITLE,
        PAGE_TITLE,
        VIDEO_ZONE,
        AUDIO_QUALITY,
        VIDEO_QUALITY,
        VIDEO_CODEC,

        // 斜杠
        SLASH = 100,

        // HyphenSeparated
        UNDERSCORE = 101, // 下划线
        HYPHEN, // 连字符
        PLUS, // 加号
        COMMA, // 逗号
        PERIOD, // 句号
        AND, // and
        NUMBER, // #
        OPEN_PAREN, // 左圆括号
        CLOSE_PAREN, // 右圆括号
        OPEN_BRACKET, // 左方括号
        CLOSE_BRACKET, // 右方括号
        OPEN_BRACE, // 左花括号
        CLOSE_brace, // 右花括号
        BLANK, // 空白符

    }
}
