using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Fitness;
using Android.Gms.Fitness.Data;
using Android.Gms.Fitness.Request;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Java.Util.Concurrent;
using System;
using System.Threading.Tasks;

namespace StepCounterPOC.Android
{
    [Activity(Label = "StepCounterPOC", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string AUTH_PENDING = "auth_state_pending";
        private const int REQUEST_OAUTH = 1;

        private bool authInProgress = false;

        private GoogleApiClient client;

        private IOnDataPointListener mListener;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (bundle != null)
            {
                authInProgress = bundle.GetBoolean(AUTH_PENDING);
            }

            buildFitnessClient();

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += (sender, args) =>
            {
            };
        }

        protected override void OnStart()
        {
            base.OnStart();
            client.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (client.IsConnected)
            {
                client.Disconnect();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_OAUTH)
            {
                authInProgress = false;
                if (resultCode == Result.Ok)
                {
                    // Make sure the app is not already connected or attempting to connect
                    if (!client.IsConnecting && !client.IsConnected)
                    {
                        client.Connect();
                    }
                }
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutBoolean(AUTH_PENDING, authInProgress);
        }

        private void buildFitnessClient()
        {
            client = new GoogleApiClient.Builder(this)
                //.AddApi(FitnessClass.SENSORS_API)
                .AddApi(FitnessClass.HISTORY_API)
                //.AddScope(new Scope(Scopes.FitnessLocationRead))
                .AddScope(new Scope(Scopes.FitnessActivityReadWrite))
                .AddConnectionCallbacks(x =>
                {
                    testSteps();
                    //testSteps2();
                    testSteps3();

                    // Now you can make calls to the Fitness APIs.
                    // Put application specific code here.
                    // [END auth_build_googleapiclient_beginning]
                    //  What to do? Find some data sources!
                    //findFitnessDataSources();

                    // [START auth_build_googleapiclient_ending]
                })
                .AddOnConnectionFailedListener(result =>
                {
                    if (!result.HasResolution)
                    {
                        GoogleApiAvailability.Instance.GetErrorDialog(this, result.ErrorCode, 0);
                        return;
                    }
                    // The failure has a resolution. Resolve it.
                    // Called typically when the app is not yet authorized, and an
                    // authorization dialog is displayed to the user.
                    if (!authInProgress)
                    {
                        try
                        {
                            authInProgress = true;
                            result.StartResolutionForResult(this, REQUEST_OAUTH);
                        }
                        catch (IntentSender.SendIntentException e)
                        {
                            var ex = e;
                        }
                    }
                })
                .Build();
        }

        private async Task findFitnessDataSources()
        {
            var dataSourcesResult = await FitnessClass.SensorsApi.FindDataSourcesAsync(client, new DataSourcesRequest.Builder()
                // At least one datatype must be specified.
                .SetDataTypes(DataType.TypeLocationSample)
                // Can specify whether data type is raw or derived.
                .SetDataSourceTypes(DataSource.TypeRaw)
                .Build());

            //Log(TAG, "Result: " + dataSourcesResult.Status);
            foreach (var dataSource in dataSourcesResult.DataSources)
            {
                //Log(TAG, "Data source found: " + dataSource);
                //Log(TAG, "Data Source type: " + dataSource.DataType.Name);

                //Let's register a listener to receive Activity data!
                if (dataSource.DataType.Name.Equals(DataType.TypeLocationSample.Name) && mListener == null)
                {
                    //Log(TAG, "Data source for LOCATION_SAMPLE found!  Registering.");
                    await registerFitnessDataListener(dataSource, DataType.TypeLocationSample);
                }
            }
        }

        private async Task testSteps()
        {
            var total = 0;

            var result = await FitnessClass.HistoryApi.ReadDailyTotalAsync(client, DataType.TypeStepCountDelta); ;

            if (result.Status.IsSuccess)
            {
                DataSet totalSet = result.Total;

                if (!totalSet.IsEmpty)
                {
                    total = totalSet.DataPoints[0].GetValue(Field.FieldSteps).AsInt();
                }
            }
            else
            {
                var a = "";
                //Log.w(TAG, "There was a problem getting the step count.");
            }
        }

        private async Task testSteps2()
        {
            DataSource dataSource = new DataSource.Builder()
                .SetAppPackageName("com.google.android.gms")
                .SetDataType(DataType.TypeStepCountDelta)
                //.SetDataType(DataType.AggregateStepCountDelta)
                .SetType(DataSource.TypeDerived)
                .SetStreamName("estimated_steps")
                .Build();

            var cal = Calendar.Instance;
            var now = new Date();
            cal.Time = now;
            var endTime = cal.TimeInMillis;
            cal.Add(CalendarField.WeekOfYear, -1);
            var startTime = cal.TimeInMillis;

            DataReadRequest readRequest = new DataReadRequest.Builder()
                //.Aggregate(DataType.TypeStepCountDelta, DataType.AggregateStepCountDelta)
                .Aggregate(dataSource, DataType.AggregateStepCountDelta)
                .BucketByTime(1, TimeUnit.Days)
                .SetTimeRange(startTime, endTime, TimeUnit.Milliseconds)
                .Build();

            var dataReadResult = await FitnessClass.HistoryApi.ReadDataAsync(client, readRequest);

            //foreach (var dataSet in dataReadResult.DataSets)
            //{
            //    var d = dataSet.DataType;
            //}

            //var test = dataReadResult.GetDataSet(DataType.TypeStepCountDelta);

            //var aggregatedSteps = dataReadResult.GetDataSet(DataType.AggregateStepCountDelta);

            //foreach (var dataPoint in aggregatedSteps.DataPoints)
            //{
            //    var a = dataPoint.GetValue(Field.FieldSteps);
            //}

            //foreach (var bucket in dataReadResult.Buckets)
            //{
            //    var dataSet = bucket.DataSets[0];

            //    var activity = bucket.Activity;
            //    var bucketType = bucket.BucketType;
            //}

            DataSet test = dataReadResult.GetDataSet(DataType.TypeStepCountDelta);
            var stepData = dataReadResult.GetDataSet(DataType.AggregateStepCountDelta);

            foreach (var dp in test.DataPoints)
            {
                foreach (var field in dp.DataType.Fields)
                {
                    var a = dp.GetValue(field);
                }
            }

            int totalSteps = 0;

            foreach (DataPoint dp in stepData.DataPoints)
            {
                foreach (Field field in dp.DataType.Fields)
                {
                    int steps = dp.GetValue(field).AsInt();

                    totalSteps += steps;
                }
            }
        }

        private async Task testSteps3()
        {
            DataSource dataSource = new DataSource.Builder()
              .SetAppPackageName("com.google.android.gms")
              .SetDataType(DataType.TypeStepCountDelta)
              //.SetDataType(DataType.AggregateStepCountDelta)
              .SetType(DataSource.TypeDerived)
              .SetStreamName("estimated_steps")
              .Build();

            var cal = Calendar.Instance;
            var now = new Date();
            cal.Time = now;
            var endTime = cal.TimeInMillis;
            cal.Add(CalendarField.WeekOfYear, -1);
            var startTime = cal.TimeInMillis;

            DataReadRequest readRequest = new DataReadRequest.Builder()
                //.Aggregate(DataType.TypeStepCountDelta, DataType.AggregateStepCountDelta)
                //.Aggregate(dataSource, DataType.AggregateStepCountDelta)
                .Aggregate(dataSource, DataType.TypeStepCountDelta)
                .BucketByTime(1, TimeUnit.Days)
                .SetTimeRange(startTime, endTime, TimeUnit.Milliseconds)
                .Build();

            var dataReadResult = await FitnessClass.HistoryApi.ReadDataAsync(client, readRequest);

            if (dataReadResult.Status.IsSuccess)
            {
                foreach (var item in dataReadResult.DataSets)
                {
                    var a = item;
                }

                foreach (Bucket item in dataReadResult.Buckets)
                {
                    var t = item.DataSets;
                    var a = item;

                    foreach (var ds in item.DataSets)
                    {
                        foreach (var dp in ds.DataPoints)
                        {
                            foreach (var field in dp.DataType.Fields)
                            {
                                var b = dp.GetValue(field);
                            }
                        }
                    }
                }
            }
        }

        private async Task registerFitnessDataListener(DataSource dataSource, DataType dataType)
        {
            mListener = new DataPointListener(dataPoint =>
            {
                foreach (var field in dataPoint.DataType.Fields)
                {
                    var val = dataPoint.GetValue(field);
                    //Log(TAG, "Detected DataPoint field: " + field.Name);
                    //Log(TAG, "Detected DataPoint value: " + val);
                }
            });

            var status = await FitnessClass.SensorsApi.AddAsync(
                 client,
                 new SensorRequest.Builder()
                .SetDataSource(dataSource) // Optional but recommended for custom data sets.
                .SetDataType(dataType) // Can't be omitted.
                .SetSamplingRate(10, Java.Util.Concurrent.TimeUnit.Seconds)
                .Build(),
                 mListener);

            if (status.IsSuccess)
            {
                //Log(TAG, "Listener registered!");
            }
            else
            {
                //Log(TAG, "Listener not registered.");
            }
        }

        private async Task unregisterFitnessDataListener()
        {
            if (mListener == null)
            {
                // This code only activates one listener at a time.  If there's no listener, there's
                // nothing to unregister.
                return;
            }

            // [START unregister_data_listener]
            // Waiting isn't actually necessary as the unregister call will complete regardless,
            // even if called from within onStop, but a callback can still be added in order to
            // inspect the results.
            var status = await FitnessClass.SensorsApi.RemoveAsync(
                             client,
                             mListener);

            if (status.IsSuccess)
            {
                //Log(TAG, "Listener was removed!");
            }
            else
            {
                //Log(TAG, "Listener was not removed.");
            }
        }
    }

    internal class DataPointListener : Java.Lang.Object, IOnDataPointListener
    {
        public DataPointListener(Action<DataPoint> dataPointHandler)
        {
            DataPointHandler = dataPointHandler;
        }

        public Action<DataPoint> DataPointHandler { get; private set; }

        public void OnDataPoint(DataPoint dataPoint)
        {
            var h = DataPointHandler;
            if (h != null)
                h(dataPoint);
        }
    }
}