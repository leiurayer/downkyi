using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Zone
{
    public class VideoZone
    {
        private static VideoZone that;
        private readonly List<ZoneAttr> zones = new List<ZoneAttr>();

        /// <summary>
        /// 使用单例模式获取分区，注意未搜索到的分区需要额外处理
        /// </summary>
        /// <returns></returns>
        public static VideoZone Instance()
        {
            if (that == null)
            {
                that = new VideoZone();
            }
            return that;
        }

        public List<ZoneAttr> GetZones()
        {
            return zones;
        }

        private VideoZone()
        {
            //动画
            zones.Add(new ZoneAttr(1, "douga", "动画")); // 主分区
            zones.Add(new ZoneAttr(24, "mad", "MAD·AMV", 1)); //具有一定制作程度的动画或静画的二次创作视频
            zones.Add(new ZoneAttr(25, "mmd", "MMD·3D", 1)); //使用MMD（MikuMikuDance）和其他3D建模类软件制作的视频
            zones.Add(new ZoneAttr(47, "voice", "短片·手书·配音", 1)); //追求创新并具有强烈特色的短片、手书（绘）及ACG相关配音
            zones.Add(new ZoneAttr(210, "garage_kit", "手办·模玩", 1)); //手办模玩的测评、改造或其他衍生内容
            zones.Add(new ZoneAttr(86, "tokusatsu", "特摄", 1)); //特摄相关衍生视频
            zones.Add(new ZoneAttr(27, "other", "综合", 1)); //以动画及动画相关内容为素材，包括但不仅限于音频替换、杂谈、排行榜等内容

            //番剧
            zones.Add(new ZoneAttr(13, "anime", "番剧")); // 主分区
            zones.Add(new ZoneAttr(33, "serial", "连载动画", 13)); //当季连载的动画番剧
            zones.Add(new ZoneAttr(32, "finish", "完结动画", 13)); //已完结的动画番剧合集
            zones.Add(new ZoneAttr(51, "information", "资讯", 13)); //动画番剧相关资讯视频
            zones.Add(new ZoneAttr(152, "offical", "官方延伸", 13)); //动画番剧为主题的宣传节目、采访视频，及声优相关视频

            //国创
            zones.Add(new ZoneAttr(167, "guochuang", "国创")); // 主分区
            zones.Add(new ZoneAttr(153, "chinese", "国产动画", 167)); //我国出品的PGC动画
            zones.Add(new ZoneAttr(168, "original", "国产原创相关", 167)); //
            zones.Add(new ZoneAttr(169, "puppetry", "布袋戏", 167)); //
            zones.Add(new ZoneAttr(195, "motioncomic", "动态漫·广播剧", 167)); //
            zones.Add(new ZoneAttr(170, "information", "资讯", 167)); //

            //音乐
            zones.Add(new ZoneAttr(3, "music", "音乐")); // 主分区
            zones.Add(new ZoneAttr(28, "original", "原创音乐", 3)); //个人或团队制作以音乐为主要原创因素的歌曲或纯音乐
            zones.Add(new ZoneAttr(31, "cover", "翻唱", 3)); //一切非官方的人声再演绎歌曲作品
            zones.Add(new ZoneAttr(30, "vocaloid", "VOCALOID·UTAU", 3)); //以雅马哈Vocaloid和UTAU引擎为基础，包含其他调教引擎，运用各类音源进行的歌曲创作内容
            zones.Add(new ZoneAttr(194, "electronic", "电音", 3)); //以电子合成器、音乐软体等产生的电子声响制作的音乐
            zones.Add(new ZoneAttr(59, "perform", "演奏", 3)); //传统或非传统乐器及器材的演奏作品
            zones.Add(new ZoneAttr(193, "mv", "MV", 3)); //音乐录影带，为搭配音乐而拍摄或制作的视频
            zones.Add(new ZoneAttr(29, "live", "音乐现场", 3)); //音乐实况表演视频
            zones.Add(new ZoneAttr(130, "other", "音乐综合", 3)); //收录无法定义到其他音乐子分区的音乐视频

            //舞蹈
            zones.Add(new ZoneAttr(129, "dance", "舞蹈")); // 主分区
            zones.Add(new ZoneAttr(20, "otaku", "宅舞", 129)); //与ACG相关的翻跳、原创舞蹈
            zones.Add(new ZoneAttr(198, "hiphop", "街舞", 129)); //收录街舞相关内容，包括赛事现场、舞室作品、个人翻跳、FREESTYLE等
            zones.Add(new ZoneAttr(199, "star", "明星舞蹈", 129)); //国内外明星发布的官方舞蹈及其翻跳内容
            zones.Add(new ZoneAttr(200, "china", "中国舞", 129)); //传承中国艺术文化的舞蹈内容，包括古典舞、民族民间舞、汉唐舞、古风舞等
            zones.Add(new ZoneAttr(154, "three_d", "舞蹈综合", 129)); //收录无法定义到其他舞蹈子分区的舞蹈视频
            zones.Add(new ZoneAttr(156, "demo", "舞蹈教程", 129)); //镜面慢速，动作分解，基础教程等具有教学意义的舞蹈视频

            //游戏
            zones.Add(new ZoneAttr(4, "game", "游戏")); // 主分区
            zones.Add(new ZoneAttr(17, "stand_alone", "单机游戏", 4)); //以所有平台（PC、主机、移动端）的单机或联机游戏为主的视频内容，包括游戏预告、CG、实况解说及相关的评测、杂谈与视频剪辑等
            zones.Add(new ZoneAttr(171, "esports", "电子竞技", 4)); //具有高对抗性的电子竞技游戏项目，其相关的赛事、实况、攻略、解说、短剧等视频。
            zones.Add(new ZoneAttr(172, "mobile", "手机游戏", 4)); //以手机及平板设备为主要平台的游戏，其相关的实况、攻略、解说、短剧、演示等视频。
            zones.Add(new ZoneAttr(65, "online", "网络游戏", 4)); //由网络运营商运营的多人在线游戏，以及电子竞技的相关游戏内容。包括赛事、攻略、实况、解说等相关视频
            zones.Add(new ZoneAttr(173, "board", "桌游棋牌", 4)); //桌游、棋牌、卡牌对战等及其相关电子版游戏的实况、攻略、解说、演示等视频。
            zones.Add(new ZoneAttr(121, "gmv", "GMV", 4)); //由游戏素材制作的MV视频。以游戏内容或CG为主制作的，具有一定创作程度的MV类型的视频
            zones.Add(new ZoneAttr(136, "music", "音游", 4)); //各个平台上，通过配合音乐与节奏而进行的音乐类游戏视频
            zones.Add(new ZoneAttr(19, "mugen", "Mugen", 4)); //以Mugen引擎为平台制作、或与Mugen相关的游戏视频

            //知识
            zones.Add(new ZoneAttr(36, "knowledge", "知识")); // 主分区
            zones.Add(new ZoneAttr(201, "science", "科学科普", 36)); //回答你的十万个为什么
            zones.Add(new ZoneAttr(124, "social_science", "社科·法律·心理", 36)); //基于社会科学、法学、心理学展开或个人观点输出的知识视频
            zones.Add(new ZoneAttr(228, "humanity_history", "人文历史", 36)); //看看古今人物，聊聊历史过往，品品文学典籍
            zones.Add(new ZoneAttr(207, "business", "财经商业", 36)); //说金融市场，谈宏观经济，一起畅聊商业故事
            zones.Add(new ZoneAttr(208, "campus", "校园学习", 36)); //老师很有趣，学生也有才，我们一起搞学习
            zones.Add(new ZoneAttr(209, "career", "职业职场", 36)); //职业分享、升级指南，一起成为最有料的职场人
            zones.Add(new ZoneAttr(229, "design", "设计·创意", 36)); //天马行空，创意设计，都在这里
            zones.Add(new ZoneAttr(122, "skill", "野生技能协会", 36)); //技能党集合，是时候展示真正的技术了

            //科技
            zones.Add(new ZoneAttr(188, "tech", "科技")); // 主分区
            zones.Add(new ZoneAttr(95, "digital", "数码", 188)); //科技数码产品大全，一起来做发烧友
            zones.Add(new ZoneAttr(230, "application", "软件应用", 188)); //超全软件应用指南
            zones.Add(new ZoneAttr(231, "computer_tech", "计算机技术", 188)); //研究分析、教学演示、经验分享......有关计算机技术的都在这里
            zones.Add(new ZoneAttr(232, "industry", "工业·工程·机械", 188)); //前方高能，机甲重工即将出没
            zones.Add(new ZoneAttr(233, "diy", "极客DIY", 188)); //炫酷技能，极客文化，硬核技巧，准备好你的惊讶

            //运动
            zones.Add(new ZoneAttr(234, "sports", "运动")); // 主分区
            zones.Add(new ZoneAttr(235, "basketballfootball", "篮球·足球", 234)); //与篮球、足球相关的视频，包括但不限于篮足球赛事、教学、评述、剪辑、剧情等相关内容
            zones.Add(new ZoneAttr(164, "aerobics", "健身", 234)); //与健身相关的视频，包括但不限于瑜伽、CrossFit、健美、力量举、普拉提、街健等相关内容
            zones.Add(new ZoneAttr(236, "athletic", "竞技体育", 234)); //与竞技体育相关的视频，包括但不限于乒乓、羽毛球、排球、赛车等竞技项目的赛事、评述、剪辑、剧情等相关内容
            zones.Add(new ZoneAttr(237, "culture", "运动文化", 234)); //与运动文化相关的视频，包络但不限于球鞋、球衣、球星卡等运动衍生品的分享、解读，体育产业的分析、科普等相关内容
            zones.Add(new ZoneAttr(238, "comprehensive", "运动综合", 234)); //与运动综合相关的视频，包括但不限于钓鱼、骑行、滑板等日常运动分享、教学、Vlog等相关内容

            //汽车
            zones.Add(new ZoneAttr(223, "car", "汽车")); // 主分区
            zones.Add(new ZoneAttr(176, "life", "汽车生活", 223)); //分享汽车及出行相关的生活体验类视频
            zones.Add(new ZoneAttr(224, "culture", "汽车文化", 223)); //车迷的精神圣地，包括汽车赛事、品牌历史、汽车改装、经典车型和汽车模型等
            zones.Add(new ZoneAttr(225, "geek", "汽车极客", 223)); //汽车硬核达人聚集地，包括DIY造车、专业评测和技术知识分享
            zones.Add(new ZoneAttr(240, "motorcycle", "摩托车", 223)); //骑士们集合啦
            zones.Add(new ZoneAttr(226, "smart", "智能出行", 223)); //探索新能源汽车和未来智能出行的前沿阵地
            zones.Add(new ZoneAttr(227, "strategy", "购车攻略", 223)); //丰富详实的购车建议和新车体验

            //生活
            zones.Add(new ZoneAttr(160, "life", "生活")); // 主分区
            zones.Add(new ZoneAttr(138, "funny", "搞笑", 160)); //各种沙雕有趣的搞笑剪辑，挑战，表演，配音等视频
            zones.Add(new ZoneAttr(239, "home", "家居房产", 160)); //与买房、装修、居家生活相关的分享
            zones.Add(new ZoneAttr(161, "handmake", "手工", 160)); //手工制品的制作过程或成品展示、教程、测评类视频
            zones.Add(new ZoneAttr(162, "painting", "绘画", 160)); //绘画过程或绘画教程，以及绘画相关的所有视频
            zones.Add(new ZoneAttr(21, "daily", "日常", 160)); //记录日常生活，分享生活故事

            //美食
            zones.Add(new ZoneAttr(211, "food", "美食")); // 主分区
            zones.Add(new ZoneAttr(76, "make", "美食制作", 211)); //学做人间美味，展示精湛厨艺
            zones.Add(new ZoneAttr(212, "detective", "美食侦探", 211)); //寻找美味餐厅，发现街头美食
            zones.Add(new ZoneAttr(213, "measurement", "美食测评", 211)); //吃货世界，品尝世间美味
            zones.Add(new ZoneAttr(214, "rural", "田园美食", 211)); //品味乡野美食，寻找山与海的味道
            zones.Add(new ZoneAttr(215, "record", "美食记录", 211)); //记录一日三餐，给生活添一点幸福感

            //动物圈
            zones.Add(new ZoneAttr(217, "animal", "动物圈")); // 主分区
            zones.Add(new ZoneAttr(218, "cat", "喵星人", 217)); //喵喵喵喵喵
            zones.Add(new ZoneAttr(219, "dog", "汪星人", 217)); //汪汪汪汪汪
            zones.Add(new ZoneAttr(220, "panda", "大熊猫", 217)); //芝麻汤圆营业中
            zones.Add(new ZoneAttr(221, "wild_animal", "野生动物", 217)); //内有“猛兽”出没
            zones.Add(new ZoneAttr(222, "reptiles", "爬宠", 217)); //鳞甲有灵
            zones.Add(new ZoneAttr(75, "animal_composite", "动物综合", 217)); //收录除上述子分区外，其余动物相关视频以及非动物主体或多个动物主体的动物相关延伸内容

            //鬼畜
            zones.Add(new ZoneAttr(119, "kichiku", "鬼畜")); // 主分区
            zones.Add(new ZoneAttr(22, "guide", "鬼畜调教", 119)); //使用素材在音频、画面上做一定处理，达到与BGM一定的同步感
            zones.Add(new ZoneAttr(26, "mad", "音MAD", 119)); //使用素材音频进行一定的二次创作来达到还原原曲的非商业性质稿件
            zones.Add(new ZoneAttr(126, "manual_vocaloid", "人力VOCALOID", 119)); //将人物或者角色的无伴奏素材进行人工调音，使其就像VOCALOID一样歌唱的技术
            zones.Add(new ZoneAttr(216, "theatre", "鬼畜剧场", 119)); //使用素材进行人工剪辑编排的有剧情的作品
            zones.Add(new ZoneAttr(127, "course", "教程演示", 119)); //鬼畜相关的教程演示

            //时尚
            zones.Add(new ZoneAttr(155, "fashion", "时尚")); // 主分区
            zones.Add(new ZoneAttr(157, "makeup", "美妆护肤", 155)); //彩妆护肤、美甲美发、仿妆、医美相关内容分享或产品测评
            zones.Add(new ZoneAttr(158, "clothing", "穿搭", 155)); //穿搭风格、穿搭技巧的展示分享，涵盖衣服、鞋靴、箱包配件、配饰（帽子、钟表、珠宝首饰）等
            zones.Add(new ZoneAttr(159, "trend", "时尚潮流", 155)); //时尚街拍、时装周、时尚大片，时尚品牌、潮流等行业相关记录及知识科普

            //资讯
            zones.Add(new ZoneAttr(202, "information", "资讯")); // 主分区
            zones.Add(new ZoneAttr(203, "hotspot", "热点", 202)); //全民关注的时政热门资讯
            zones.Add(new ZoneAttr(204, "global", "环球", 202)); //全球范围内发生的具有重大影响力的事件动态
            zones.Add(new ZoneAttr(205, "social", "社会", 202)); //日常生活的社会事件、社会问题、社会风貌的报道
            zones.Add(new ZoneAttr(206, "multiple", "综合", 202)); //除上述领域外其它垂直领域的综合资讯

            //娱乐
            zones.Add(new ZoneAttr(5, "ent", "娱乐")); // 主分区
            zones.Add(new ZoneAttr(71, "variety", "综艺", 5)); //所有综艺相关，全部一手掌握！
            zones.Add(new ZoneAttr(241, "talker", "娱乐杂谈", 5)); //娱乐人物解读、娱乐热点点评、娱乐行业分析
            zones.Add(new ZoneAttr(242, "fans", "粉丝创作", 5)); //粉丝向创作视频
            zones.Add(new ZoneAttr(137, "celebrity", "明星综合", 5)); //娱乐圈动态、明星资讯相关

            //影视
            zones.Add(new ZoneAttr(181, "cinephile", "影视")); // 主分区
            zones.Add(new ZoneAttr(182, "cinecism", "影视杂谈", 181)); //影视评论、解说、吐槽、科普等
            zones.Add(new ZoneAttr(183, "montage", "影视剪辑", 181)); //对影视素材进行剪辑再创作的视频
            zones.Add(new ZoneAttr(85, "shortfilm", "短片", 181)); //追求自我表达且具有特色的短片
            zones.Add(new ZoneAttr(184, "trailer_info", "预告·资讯", 181)); //影视类相关资讯，预告，花絮等视频

            //纪录片
            zones.Add(new ZoneAttr(177, "documentary", "纪录片")); // 主分区
            zones.Add(new ZoneAttr(37, "history", "人文·历史", 177)); //
            zones.Add(new ZoneAttr(178, "science", "科学·探索·自然", 177)); //
            zones.Add(new ZoneAttr(179, "military", "军事", 177)); //
            zones.Add(new ZoneAttr(180, "travel", "社会·美食·旅行", 177)); //

            //电影
            zones.Add(new ZoneAttr(23, "movie", "电影")); // 主分区
            zones.Add(new ZoneAttr(147, "chinese", "华语电影", 23)); //
            zones.Add(new ZoneAttr(145, "west", "欧美电影", 23)); //
            zones.Add(new ZoneAttr(146, "japan", "日本电影", 23)); //
            zones.Add(new ZoneAttr(83, "movie", "其他国家", 23)); //

            //电视剧
            zones.Add(new ZoneAttr(11, "tv", "电视剧")); // 主分区
            zones.Add(new ZoneAttr(185, "mainland", "国产剧", 11)); //
            zones.Add(new ZoneAttr(187, "overseas", "海外剧", 11)); //

        }
    }
}
