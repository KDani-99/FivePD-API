using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivePD.API
{
    public abstract class Events
    {

        public delegate Task OnDutyStatusChangeDelegate(bool onDuty);
        public static event OnDutyStatusChangeDelegate OnDutyStatusChange;

        public delegate Task OnServiceCalledDelegate(Utilities.Services service);
        public static event OnServiceCalledDelegate OnServiceCalled;

        public delegate Task OnCalloutReceivedDelegate(Callout callout);
        public static event OnCalloutReceivedDelegate OnCalloutReceived;

        public delegate Task OnCalloutAcceptedDelegate(Callout callout);
        public static event OnCalloutAcceptedDelegate OnCalloutAccepted;

        public delegate Task OnCalloutCompletedDelegate(Callout callout);
        public static event OnCalloutCompletedDelegate OnCalloutCompleted;

        public static void InvokeDutyEvent(bool onDuty)
        {
            OnDutyStatusChange?.Invoke(onDuty);
        }
        public static void InvokeServiceEvent(Utilities.Services service)
        {
            OnServiceCalled?.Invoke(service);
        }
        public static void InvokeCalloutReceivedEvent(Callout callout)
        {
            OnCalloutReceived?.Invoke(callout);
        }
        public static void InvokeCalloutAcceptedEvent(Callout callout)
        {
            OnCalloutAccepted?.Invoke(callout);
        }
        public static void InvokeCalloutCompletedEvent(Callout callout)
        {
            OnCalloutCompleted?.Invoke(callout);
        }
        // Func<bool,Task> OnDutyStatusChange;
    }
}
