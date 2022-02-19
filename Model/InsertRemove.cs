using System.Management;

namespace MVVM_Com
{
    internal class InsertRemove
    {
        internal event Delegate EventInsert;

        internal event Delegate EventRemove;

        internal InsertRemove()
        {
            // ManagementEventWatcher подписывается на временные уведомления о событиях согласно заданному запросу событий
            ManagementEventWatcher insertWatcher = new ManagementEventWatcher();
            ManagementEventWatcher removeWatcher = new ManagementEventWatcher();

            // WqlEventQuery делает запрос WMI событий в формате WQL
            WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            WqlEventQuery removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");

            insertWatcher.Query = insertQuery;
            removeWatcher.Query = removeQuery;

            insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceConnectEvent);
            removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);

            insertWatcher.Start();
            removeWatcher.Start();
        }

        private void DeviceConnectEvent(object sender, EventArrivedEventArgs e)
        {
            EventInsert(); //Создаем событие подключения/отключения носителя
        }

        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            //Создаем событие подключения/отключения носителя
            EventRemove();
        }
    }
}