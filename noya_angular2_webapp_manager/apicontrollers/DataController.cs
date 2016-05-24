﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using noya_angular2_webapp_manager.Dal;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace noya_angular2_webapp_manager.apicontrollers
{
    public class DataController : ApiController
    {
        [AcceptVerbs("Post")]
        public UpdateResponse UpdateMenuItems(dynamic request)
        {
            var dataRequest = this.ConvertStupidArgumentToNormalUpdateRequset<UpdateMenuRequest>(request) as UpdateMenuRequest;
            using (SqlConnection connection = initializeTestConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("SampleTransaction");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    foreach (var item in dataRequest.MenuItems)
                    {
                        command.Parameters.Clear();
                        command.CommandText = "MenuItemsUpdate";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", item.ID);
                        command.Parameters.AddWithValue("@Text_English", item.Text_English);
                        command.Parameters.AddWithValue("@Text_Hebrew", item.Text_Hebrew);
                        command.Parameters.AddWithValue("@Order", item.Order);
                        command.Parameters.AddWithValue("@IsDefault", item.isDefault);
                        command.Parameters.AddWithValue("@ToDelete", item.ToDelete);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return new UpdateResponse() { UpdateStatus = UpdateStatus.Success };
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        return new UpdateResponse() { UpdateStatus = UpdateStatus.Fail };
                    }
                    return new UpdateResponse() { UpdateStatus = UpdateStatus.Fail };
                }
            }
        }

        [AcceptVerbs("Post")]
        public UpdateResponse UpdateLinks(dynamic request)
        {
            var dataRequest = this.ConvertStupidArgumentToNormalUpdateRequset<UpdateLinksRequest>(request) as UpdateLinksRequest;
            using (SqlConnection connection = initializeTestConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("SampleTransaction");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    foreach (var item in dataRequest.Links)
                    {
                        command.Parameters.Clear();
                        command.CommandText = "LinksUpdate";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", item.ID);
                        command.Parameters.AddWithValue("@Address_Eng", item.Address_Eng);
                        command.Parameters.AddWithValue("@Address_Heb", item.Address_Heb);
                        command.Parameters.AddWithValue("@Order", item.Order);
                        command.Parameters.AddWithValue("@Text_Eng", item.Text_Eng);
                        command.Parameters.AddWithValue("@Text_Heb", item.Text_Heb);
                        command.Parameters.AddWithValue("@ToDelete", item.ToDelete);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return new UpdateResponse() { UpdateStatus = UpdateStatus.Success };
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        return new UpdateResponse() { UpdateStatus = UpdateStatus.Fail };
                    }
                    return new UpdateResponse() { UpdateStatus = UpdateStatus.Fail };
                }
            }
        }

        [AcceptVerbs("Post")]
        public UpdateResponse UpdateBiography(dynamic request)
        {
            var updateRequest = this.ConvertStupidArgumentToNormalUpdateRequset<UpdateCVRequest>(request) as UpdateCVRequest;
            using (SqlConnection connection = initializeTestConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("SampleTransaction");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    foreach (var item in updateRequest.CVs)
                    {
                        command.Parameters.Clear();
                        command.CommandText = "CVUpdate";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", item.ID);
                        command.Parameters.AddWithValue("@Heb", item.Heb);
                        command.Parameters.AddWithValue("@Eng", item.Eng);
                        command.Parameters.AddWithValue("@ToDelete", item.ToDelete);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return new UpdateResponse() { UpdateStatus = UpdateStatus.Success };
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        return new UpdateResponse() { UpdateStatus = UpdateStatus.Fail };
                    }
                    return new UpdateResponse() { UpdateStatus = UpdateStatus.Fail };
                }
            }
        }


        [AcceptVerbs("Post")]
        public MenuResponse GetMenuItems(dynamic request)
        {
            var dataRequest = this.ConvertStupidArgumentToNormalDataRequset<DataRequest>(request);

            SqlConnection connection = initializeTestConnection();
            var res = new MenuResponse();
            var menuItemsList = new List<MenuItem>();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("MenuItemsSelect", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        double order = Convert.ToDouble(reader["Order"]);
                        int id = Convert.ToInt32(reader["ID"]);
                        string text_English = reader["Text_English"].ToString();
                        string Text_Hebrew = reader["Text_Hebrew"].ToString();
                        bool isDefault = bool.Parse(reader["IsDefault"].ToString());
                        string name = reader["Name"].ToString();
                        var item = new MenuItem(id, order, text_English, Text_Hebrew, isDefault, name);
                        menuItemsList.Add(item);
                    }
                }
                res.MenuItems = menuItemsList.ToArray();
                return res;
            }


            finally
            {
                connection.Close();
            }
        }

        [AcceptVerbs("Post")]
        public LinksResponse GetLinks(dynamic request)
        {
            var dataRequest = this.ConvertStupidArgumentToNormalDataRequset<DataRequest>(request);
            LinksResponse res = new LinksResponse();
            SqlConnection connection = initializeTestConnection();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("LinksSelect_Manager", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                List<Link> linksList = new List<Link>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = Convert.ToInt32(reader["ID"].ToString());
                        var text_heb = reader["Text_Heb"].ToString();
                        var address_heb = reader["Address_Heb"].ToString();
                        var text_eng = reader["Text_Eng"].ToString();
                        var address_eng = reader["Address_Eng"].ToString();
                        var order = Convert.ToDouble(reader["Order"].ToString());
                        var timestamp = Convert.ToDateTime(reader["TimeStamp"].ToString());

                        var link = new Link(id, text_heb, address_heb, text_eng, address_eng, order, timestamp, false);
                        linksList.Add(link);
                    }
                }
                res.Links = linksList.ToArray();
                return res;
            }
            finally
            {
                connection.Close();
            }
        }


        [AcceptVerbs("Post")]
        public UpdateRsponse GetUpdates(object request)
        {

            var calendarRequest = this.ConvertStupidArgumentToNormalDataRequset<DataRequest>(request);
            SqlConnection con = initializeTestConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("HotUpdatesSelect", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lang", "Data_Eng");
                List<Update> updates = new List<Update>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Update update = new Update()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Order = Convert.ToDouble(reader["Order"]),
                            Data_Heb = reader["Data_Heb"].ToString(),
                            Data_Eng = reader["Data_Eng"].ToString(),
                            TimeStamp = Convert.ToDateTime(reader["TimeStamp"]),
                        };
                        updates.Add(update);
                    }
                }
                return new UpdateRsponse() { Updates = updates.ToArray() };

            }
            finally
            {
                con.Close();
            }
        }

        [AcceptVerbs("Post")]
        public PressResponse GetPress(object request)
        {

            var calendarRequest = this.ConvertStupidArgumentToNormalDataRequset<DataRequest>(request);
            SqlConnection con = initializeTestConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PressSelect", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lang", "Eng");
                List<PressItem> presses = new List<PressItem>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PressItem item = new PressItem()
                        {
                            ID = Convert.ToInt32(reader["ID"]),

                            Eng = reader["Eng"].ToString(),
                            Heb = reader["Heb"].ToString(),
                            TimeStamp = Convert.ToDateTime(reader["TimeStamp"]),
                        };
                        presses.Add(item);
                    }
                }
                return new PressResponse() { PressItems = presses.ToArray() };

            }
            finally
            {
                con.Close();
            }
        }

        [AcceptVerbs("Post")]
        public CalendarResponse GetCalendar(object request)
        {
            var calendarRequest = this.ConvertStupidArgumentToNormalDataRequset<CalendarRequset>(request);
            SqlConnection con = initializeTestConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CalendarItemSelect_New", con);
                cmd.Parameters.AddWithValue("@lang", "Text_Eng");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                List<CalendarItem> list = new List<CalendarItem>();
                CalendarItem resultItem = null;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CalendarItem item = new CalendarItem();
                        item.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                        item.Visible = Convert.ToBoolean(reader["Visible"]);
                        item.Text_Eng = reader["Text_Eng"].ToString();
                        item.DataDate = Convert.ToDateTime(reader["DataDate"]).Date;
                        item.ID = Convert.ToInt32(reader["ID"]);
                        list.Add(item);
                    }
                }

                return new CalendarResponse() { CalendarItems = list.ToArray() };
            }
            finally
            {
                con.Close();
            }
        }

        private static SqlConnection initializeTestConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["testDB"].ConnectionString);

            return connection;
        }

        [AcceptVerbs("Post")]
        public ProgramsResponse GetPrograms(DataRequest request)
        {

            SqlConnection connection = initializeTestConnection();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("ProgramsSelect", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lang", "Eng");
                List<Program> prgs = new List<Program>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Program prg = new Program();
                        prg.ID = Convert.ToInt32(reader["ID"].ToString());
                        prg.Name = reader["Name"].ToString();
                        prg.Heb = reader["Heb"].ToString();
                        prg.Eng = reader["Eng"].ToString();
                        prg.Order = Convert.ToDouble(reader["Order"].ToString());
                        prg.TimeStamp = Convert.ToDateTime(reader["TimeStamp"].ToString());
                        prgs.Add(prg);
                    }

                }
                return new ProgramsResponse() { Programs = prgs.ToArray() };
            }
            finally
            {

                connection.Close();
            }

        }

        [AcceptVerbs("Post")]
        public CVResponse GetCV(dynamic request)
        {
            var dataRequest = this.ConvertStupidArgumentToNormalDataRequset<DataRequest>(request);
            CVResponse res = new CVResponse();
            SqlConnection connection = initializeTestConnection();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("CVSelect_Manager", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                List<CV> cvSList = new List<CV>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CV cv = new CV();
                        cv.Eng = reader["Eng"].ToString();
                        cv.Heb = reader["Heb"].ToString();
                        cv.ID = Convert.ToInt32(reader["ID"].ToString());
                        cv.TimeStamp = Convert.ToDateTime(reader["TimeStamp"].ToString());
                        cvSList.Add(cv);
                    }
                }
                res.CVs = cvSList.ToArray();
                return res;
            }
            finally
            {
                connection.Close();
            }

        }

        [AcceptVerbs("Post")]
        public ImageGalleryResponse GetImages(object request)
        {

            ImageGalleryRequest imageGalleryRequest = this.ConvertStupidArgumentToNormalDataRequset<ImageGalleryRequest>(request);
            ImageGalleryItem item = null;
            SqlConnection con = initializeTestConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ImagesGallery_NewSelect", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                List<ImageGalleryItem> rawList = new List<ImageGalleryItem>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ImageGalleryItem imageGalleryItem = new ImageGalleryItem()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            ImageName = Convert.ToString(reader["ImageName"]),
                            Order = Convert.ToDouble(reader["Order"]),
                            TimeStamp = Convert.ToDateTime(reader["TimeStamp"]),
                            Visible = Convert.ToBoolean(reader["Visible"])
                        };
                        rawList.Add(imageGalleryItem);
                    }
                }

                ImageGalleryResponse imageGalleryResponse = null;


                return imageGalleryResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private T ConvertStupidArgumentToNormalDataRequset<T>(object request) where T : DataRequest
        {
            JObject jObject = JObject.Parse(request.ToString());
            var parsed = jObject.First.FirstOrDefault().ToObject<T>();
            return parsed;
        }

        private T ConvertStupidArgumentToNormalUpdateRequset<T>(object request) where T : UpdateDataRequest
        {
            JObject jObject = JObject.Parse(request.ToString());
            var parsed = jObject.First.FirstOrDefault().ToObject<T>();
            return parsed;
        }
    }
}