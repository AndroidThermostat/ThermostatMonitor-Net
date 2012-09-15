using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: If you change the interface name "IService2" here, you must also update the reference to "IService2" in Web.config.
[ServiceContract]
public interface IService2
{
    [OperationContract]
    void DoWork();

    [OperationContract]
    void StatIn(byte[] data);

}
