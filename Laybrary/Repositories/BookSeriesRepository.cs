 var model = db.BookSeries.Single(bs => bs.Id == bookSeries.Id);

                    if (!String.IsNullOrEmpty(bookSeries.Name))
                    {
                        model.Name = bookSeries.Name;
                    }
                    else
                    {
                        bookSeries.Name = model.Name;
                    }

                    if (!String.IsNullOrEmpty(bookSeries.Author))
                    {
                        model.Author = bookSeries.Author;
                    }
                    else
                    {
                        bookSeries.Author = model.Author;
                    }

                    if (bookSeries.Queue != null)
                    {
                        model.Queue = bookSeries.Queue;
                    }
                    else
                    {
                        bookSeries.Queue = model.Queue;
                    }

                    if (bookSeries.Registration_Order != null)
                    {
                        model.Registration_Order = bookSeries.Registration_Order;
                    }
                    else
                    {
                        bookSeries.Registration_Order = model.Registration_Order;
                    }

                    if (bookSeries.Total_Books != null)
                    {
                        model.Total_Books = bookSeries.Total_Books;
                    }
                    else
                    {
                        bookSeries.Total_Books = model.Total_Books;
                    }

                    if (bookSeries.Total_Read != null)
                    {
                         model.Total_Read = bookSeries.Total_Read;
                    }
                    else
                    {
                        bookSeries.Total_Read = model.Total_Read;
                    }

                    if (bookSeries.SerieStatus_Id != null)
                    {
                        model.SerieStatus_Id = bookSeries.SerieStatus_Id;
                    }
                    else
                    {
                        bookSeries.SerieStatus_Id = model.SerieStatus_Id;
                    }  
