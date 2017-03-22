using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DotNetDemos.CSharpExamples
{
    class GarbageCollection
    {
        public void WithIDisposable()
        {
            for (int i = 0; i < 100; i++)
            {
                using (var state = new UmanagedDatabaseState())
                {
                    Console.WriteLine(state.GetData());
                }

                ////This will give exception
                //var state = new UmanagedDatabaseState();
                //Console.WriteLine(state.GetData());
            }
            Console.WriteLine("The End");
        }
    }

    class DatabaseState : IDisposable
    {
        protected SqlConnection _connection;
        private bool _disposed = false;

        public virtual string GetData()
        {
            if (_disposed)
                throw new ObjectDisposedException("Datastate");

            if (_connection == null)
            {
                _connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=SCS_Tenant_Reporting_localhost;Integrated Security=True;Trusted_Connection=True;App=testapp;Max pool size=215;Connection Timeout=2;");
                _connection.Open();
            }
            using (var _command = _connection.CreateCommand())
            {
                _command.CommandText = "Select CONCAT(FirstName,' ', LastName) from [UserManagement].[User] where Username = 'superadmin'";
                return _command.ExecuteScalar().ToString();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    if (_connection != null)
                    {
                        Console.WriteLine(string.Format("Disposing SQLConnection: {0}", _connection.GetHashCode()));
                        _connection.Dispose();
                        _connection = null;
                    }
                    _disposed = true;
                }
            }
        }

        //select*
        //from[master].sys.sysprocesses
        //where program_name = 'testapp'
    }

    class UmanagedDatabaseState : DatabaseState
    {
        private SqlCommand _command;
        IntPtr _umanagedPointer;
        public override string GetData()
        {
            var data = base.GetData();
            if(_command != null)
            {
                _command = _connection.CreateCommand();
            }

            if(_umanagedPointer == IntPtr.Zero)
            {
                _umanagedPointer = Marshal.AllocHGlobal(100 * 1024 * 1024);
            }
            return data;
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(_command != null)
                {
                    _command.Dispose();
                    _command = null;
                }
            }

            if(_umanagedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_umanagedPointer);
                _umanagedPointer = IntPtr.Zero;
            }

            base.Dispose(disposing);
        }

        ~UmanagedDatabaseState()
        {
            Dispose(false);
        }
    }
}
