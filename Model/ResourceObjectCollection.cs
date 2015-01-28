using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    public class ResourceObjectCollection : DBObjectCollection<ResourceObject>
    {
        // 添加一个人
        public ResourceObject AddEmp(int empid)
        {
            string _ID = ToEmpID(empid);
            var rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceObject();
                rslt._ID = ToEmpID(empid);
                Add(rslt);
            }
            return rslt;
        }
        public string ToEmpID(int empid)
        {
            return "emp:" + empid;
        }

        // 虚拟一个人
        public ResourceObject AddVirtualEmp(WrkgrpObject wrkgrp, int index)
        {
            string _ID = "v_emp:" + wrkgrp._ID + "," + index;
            var rslt = new ResourceObject();
            rslt._ID = _ID;
            Add(rslt);
            return rslt;
        }

        public ResourceObject AddEq(int equipid)
        {
            string _ID = ToEqID(equipid);
            var rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceObject();
                rslt._ID = ToEqID(equipid);
                Add(rslt);
            }
            return rslt;
        }

        public string ToEqID(int equipid)
        {
            return "eq:" + equipid;
        }

        // 虚拟一个设备
        public ResourceObject AddVirtualEq(WrkgrpObject wrkgrp, EquipmentObject eq, int index)
        {
            string _ID = "v_eq:" + wrkgrp._ID + "," + eq._ID + "," + index;// 组内编号
            var rslt = new ResourceObject();
            rslt._ID = _ID;
            Add(rslt);
            return rslt;
        }
    }
}
