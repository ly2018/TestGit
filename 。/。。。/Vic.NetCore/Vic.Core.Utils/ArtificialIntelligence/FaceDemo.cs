﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Baidu.Aip.Demo
{
    public class FaceDemo
    {
        public static void FaceMatch(string img1,string img2,string img3)
        {
            
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var image1 = File.ReadAllBytes(img1);
            var image2 = File.ReadAllBytes(img2);
            var image3 = File.ReadAllBytes(img3);

            var images = new byte[][] {image1, image2, image3};

            // 人脸对比
            var result = client.FaceMatch(images);
        }

        public static void FaceDetect()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var image = File.ReadAllBytes("图片文件路径");
            var options = new Dictionary<string, object>()
            {
                {"face_fields", "beauty,age"}
            };
            var result = client.FaceDetect(image, options);
        }

        public static void FaceRegister()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var image1 = File.ReadAllBytes("图片文件路径");

            var result = client.User.Register(image1, "uid", "user info here", new []{"groupId"});
        }

        public static void FaceUpdate()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var image1 = File.ReadAllBytes("图片文件路径");

            var result = client.User.Update(image1, "uid", "groupId", "new user info");
        }

        public static void FaceDelete()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var result = client.User.Delete("uid");
            result = client.User.Delete("uid", new []{"group1"});
        }

        public static void FaceVerify()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var image1 = File.ReadAllBytes("图片文件路径");

            var result = client.User.Verify(image1, "uid", new []{"groupId"}, 1);
        }

        public static void FaceIdentify()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var image1 = File.ReadAllBytes("图片文件路径");

            var result = client.User.Identify(image1, new []{"groupId"}, 1, 1);
        }

        public static void UserInfo()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var result = client.User.GetInfo("uid");
        }

        public static void GroupList()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var result = client.Group.GetAllGroups(0, 100);
        }

        public static void GroupUsers()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var result = client.Group.GetUsers("groupId", 0, 100);
        }

        public static void GroupAddUser()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var result = client.Group.AddUser(new []{"toGroupId"}, "uid", "fromGroupId");
        }

        public static void GroupDeleteUser()
        {
            var client = new Baidu.Aip.Face.Face(AiKeySecret.ApiKey, AiKeySecret.SecretKey);
            var result = client.Group.DeleteUser(new []{"groupId"}, "uid");
        }

    }
}