//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.Appointments;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Diagnostics;


namespace SDKTemplate
{
    public sealed partial class Scenario2_Add : Page
    {
        private static readonly string calendarName = "bad_apple";
        private static int height = 20;
        private static int width = 30;
        private static int msPerFrame = 500;

        private MainPage rootPage = MainPage.Current;

        public Scenario2_Add()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Adds an appointment to the default appointment provider app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            AppointmentStore appointmentStore = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AppCalendarsReadWrite);
            IReadOnlyList<AppointmentCalendar> appointmentCalendars = await appointmentStore.FindAppointmentCalendarsAsync(FindAppointmentCalendarsOptions.IncludeHidden);

            AppointmentCalendar badAppleCal = null;
            foreach (AppointmentCalendar appointmentCalendar in appointmentCalendars)
            {

                if (appointmentCalendar.DisplayName == calendarName)
                {
                    badAppleCal = appointmentCalendar;
                }
            }

            if (badAppleCal == null)
            {
                badAppleCal = await appointmentStore.CreateAppointmentCalendarAsync(calendarName);
            }


            DateTime currentTime = DateTime.Now;
            DateTime thisMorning = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 0, 0, 0);
            DateTimeOffset startTime = new DateTimeOffset(thisMorning);

            IReadOnlyList<Appointment> apptPixels = await badAppleCal.FindAppointmentsAsync(startTime.AddDays(-1), new TimeSpan(2, 0, 0, 0));

            foreach (Appointment appt in apptPixels)
            {
                await badAppleCal.DeleteAppointmentAsync(appt.LocalId);
            }

            for (var i = 0; i < height; i++)
            {
                for (var ii = 0; ii < width; ii++)
                {
                    Appointment appointment = new Appointment();
                    appointment.Subject = Convert.ToChar('A' + ii) + "";
                    appointment.StartTime = startTime;
                    await badAppleCal.SaveAppointmentAsync(appointment);
                }
                startTime = startTime.AddMinutes(30);
            }
            apptPixels = await badAppleCal.FindAppointmentsAsync(startTime.AddDays(-1), new TimeSpan(2, 0, 0, 0));

            string[] data;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            // Get the "out.csv" file in the local folder
            StorageFile file = await localFolder.GetFileAsync("out.csv");

            // Open the file stream
            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                // Create a StreamReader to read from the stream
                using (StreamReader sr = new StreamReader(stream))
                {
                    data = (await sr.ReadToEndAsync()).Split('\n');
                }
            }
            int line = 0;
            while (line < data.Length)
            {
                String[] splitLine = data[line].Trim().Split(',');
                int placeInLine = 0;
                long start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                int removeLater = 0;
                for (int j = 0; j < apptPixels.Count; j++, placeInLine++)
                {
                    AppointmentBusyStatus prevStatus = apptPixels[j].BusyStatus;
                    if (splitLine[placeInLine] == "0")
                    {
                        apptPixels[j].BusyStatus = AppointmentBusyStatus.Free;
                    }
                    else
                    {
                        apptPixels[j].BusyStatus = AppointmentBusyStatus.Busy;

                    }
                    if (prevStatus != apptPixels[j].BusyStatus)
                    {
                        await badAppleCal.SaveAppointmentAsync(apptPixels[j]);
                    }
                    if (placeInLine >= splitLine.Length - 1)
                    {
                        line += 1;
                        placeInLine = -1;
                        splitLine = data[line].Trim().Split(',');
                    }
                }
                long elapsed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - start;
                if (elapsed > msPerFrame)
                {
                    Debug.WriteLine("frame " + line / height + " exceeeded alloted time by" + (elapsed - msPerFrame) + "ms.");
                    if (line / height > 3)
                    {
                        removeLater = (int)(elapsed - msPerFrame);
                    }
                }
                else
                {
                    int pause = msPerFrame - (int)elapsed;
                    if (removeLater > 0 && pause > removeLater)
                    {
                        Debug.WriteLine("removed " + removeLater + "ms from frame " + (line / height) + ".");
                        pause -= removeLater;
                        removeLater = 0;
                    }
                    await Task.Delay(pause);
                }

            }


            // // Create an Appointment that should be added the user's appointments provider app.
            // var appointment = new Windows.ApplicationModel.Appointments.Appointment();

            // // Get the selection rect of the button pressed to add this appointment
            // var rect = MainPage.GetElementRect(sender as FrameworkElement);

            // // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user's calendar.
            // // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
            // // An empty string return value indicates that the user canceled the operation before the appointment was added.
            // String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(appointment, rect, Windows.UI.Popups.Placement.Default);
            // if (appointmentId != String.Empty)
            // {
            //     rootPage.NotifyUser("Appointment Id: " + appointmentId, NotifyType.StatusMessage);
            // }
            // else
            // {
            //     rootPage.NotifyUser("Appointment not added.", NotifyType.ErrorMessage);
            // }
        }
    }
}
