/*
Navicat MySQL Data Transfer

Source Server         : ShiningStars
Source Server Version : 80019
Source Host           : localhost:3306
Source Database       : shiningstars

Target Server Type    : MYSQL
Target Server Version : 80019
File Encoding         : 65001

Date: 2021-03-16 22:01:36
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for book
-- ----------------------------
DROP TABLE IF EXISTS `book`;
CREATE TABLE `book` (
  `book_id` int NOT NULL AUTO_INCREMENT,
  `author` varchar(255) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  `image` varchar(255) DEFAULT NULL,
  `detail_id` int NOT NULL,
  PRIMARY KEY (`book_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of book
-- ----------------------------
INSERT INTO `book` VALUES ('1', '陈儒', 'Python源码剖析', 'https://img3.doubanio.com/lpic/s3435132.jpg', '0');
INSERT INTO `book` VALUES ('2', 'MarkPilgrim', 'Dive Into Python', 'https://img3.doubanio.com/lpic/s29631790.jpg', '0');
INSERT INTO `book` VALUES ('3', 'MarkPilgrim', 'Dive Into Python 3', 'https://img3.doubanio.com/lpic/s4059293.jpg', '0');

-- ----------------------------
-- Table structure for book_comments
-- ----------------------------
DROP TABLE IF EXISTS `book_comments`;
CREATE TABLE `book_comments` (
  `comment_id` int NOT NULL AUTO_INCREMENT,
  `book_id` int DEFAULT NULL,
  `comment` varchar(255) DEFAULT NULL,
  `agree_num` int DEFAULT NULL,
  PRIMARY KEY (`comment_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of book_comments
-- ----------------------------
INSERT INTO `book_comments` VALUES ('1', '1', '好', '2');
INSERT INTO `book_comments` VALUES ('2', '1', '很好', null);
INSERT INTO `book_comments` VALUES ('3', '1', '非常好', '3');
INSERT INTO `book_comments` VALUES ('4', '1', '非常特别的好', '0');
INSERT INTO `book_comments` VALUES ('5', '1', '非常极其特别的好', '324');
INSERT INTO `book_comments` VALUES ('6', '1', '非常极其特别无敌的好', '12');

-- ----------------------------
-- Table structure for book_detail
-- ----------------------------
DROP TABLE IF EXISTS `book_detail`;
CREATE TABLE `book_detail` (
  `detail_id` int NOT NULL AUTO_INCREMENT,
  `book_id` int DEFAULT NULL,
  `binding` varchar(255) DEFAULT NULL,
  `category` varchar(255) DEFAULT NULL,
  `image_large` varchar(255) DEFAULT NULL,
  `isbn` varchar(255) DEFAULT NULL,
  `pages` int DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `pubdate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `publisher` varchar(255) DEFAULT NULL,
  `subtitle` varchar(255) DEFAULT NULL,
  `summary` varchar(2000) DEFAULT NULL,
  `translator` varchar(2000) DEFAULT NULL,
  `author` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`detail_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of book_detail
-- ----------------------------
INSERT INTO `book_detail` VALUES ('1', '1', '平装', '算法', 'https://img1.doubanio.com/lpic/s4368169.jpg', '9787115227430', '1038', '149.00', '2021-03-03 23:22:35', '人民邮电出版社', '全球开源社区集体智慧结晶，领略Linux内核的绝美风光', '众所周知，Linux操作系统的源代码复杂、文档少，对程序员的要求高，要想看懂这些代码并不是一件容易事...', '郭旭', 'Wolfgang Mauerer');

-- ----------------------------
-- Table structure for book_member_like
-- ----------------------------
DROP TABLE IF EXISTS `book_member_like`;
CREATE TABLE `book_member_like` (
  `book_like_id` int NOT NULL AUTO_INCREMENT,
  `book_id` int NOT NULL,
  `member_id` int NOT NULL,
  PRIMARY KEY (`book_like_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of book_member_like
-- ----------------------------

-- ----------------------------
-- Table structure for journal
-- ----------------------------
DROP TABLE IF EXISTS `journal`;
CREATE TABLE `journal` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '期刊在数据中序号，供点赞使用',
  `type` int NOT NULL COMMENT '期刊类型,这里的类型分为:100 电影 200 音乐 300 句子',
  `title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '期刊题目',
  `pubdate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '发布日期',
  `index` int DEFAULT NULL COMMENT '期号',
  `image` varchar(1000) DEFAULT NULL,
  `content` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of journal
-- ----------------------------
INSERT INTO `journal` VALUES ('1', '1', 'Shining Stars', '2020-04-29 18:51:10', '5', 'images/Jessica.jpg', '浩瀚群星，璀璨如你');
INSERT INTO `journal` VALUES ('2', '2', 'You are my Shining Star', '2020-04-29 18:51:12', '4', 'images/Charlotte.jpg', '昔我往矣');
INSERT INTO `journal` VALUES ('3', '1', 'May the star be with you', '2020-04-29 19:36:44', '3', 'images/Sara.jpg', '雨雪霏霏');
INSERT INTO `journal` VALUES ('4', '1', 'Shining Stars', '2020-04-29 19:36:47', '2', 'images/Joy.jpg', '今我来思');
INSERT INTO `journal` VALUES ('5', '1', 'You are my Shining Star', '2020-04-29 19:40:19', '1', 'images/All.png', '杨柳依依');

-- ----------------------------
-- Table structure for journal_member_likes
-- ----------------------------
DROP TABLE IF EXISTS `journal_member_likes`;
CREATE TABLE `journal_member_likes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Jornal_Id` int NOT NULL,
  `Member_Id` int NOT NULL,
  `CreatedTime` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of journal_member_likes
-- ----------------------------
INSERT INTO `journal_member_likes` VALUES ('4', '4', '1', null);
INSERT INTO `journal_member_likes` VALUES ('5', '5', '1', null);

-- ----------------------------
-- Table structure for leetest
-- ----------------------------
DROP TABLE IF EXISTS `leetest`;
CREATE TABLE `leetest` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Remark` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of leetest
-- ----------------------------
INSERT INTO `leetest` VALUES ('1', 'Lee', null);
INSERT INTO `leetest` VALUES ('2', 'Zoe', null);
INSERT INTO `leetest` VALUES ('3', 'Lynn', null);

-- ----------------------------
-- Table structure for memberinfo
-- ----------------------------
DROP TABLE IF EXISTS `memberinfo`;
CREATE TABLE `memberinfo` (
  `MemberId` int NOT NULL AUTO_INCREMENT,
  `MemberNo` int DEFAULT NULL,
  `ClubId` int DEFAULT NULL,
  `MemberName` varchar(255) DEFAULT NULL,
  `TitleId` int DEFAULT NULL,
  `Gender` smallint DEFAULT NULL,
  `MemberStatus` varchar(255) DEFAULT NULL,
  `ChineseName` varchar(255) DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedTime` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `UpdatedBy` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`MemberId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of memberinfo
-- ----------------------------
INSERT INTO `memberinfo` VALUES ('1', '7', '1', 'Lee.Wang', '1', '0', '1', '王力', null, null, null, null);
