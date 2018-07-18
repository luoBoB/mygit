<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入设备名称" v-model="DeviceName">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="historyDataList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
        <el-table-column type="expand">
          <template slot-scope="props">
            <el-form label-position="left" inline class="demo-table-expand">
              <el-form-item label="设备ID">
                <span>{{ props.row.DeviceId }}</span>
              </el-form-item>
            </el-form>
          </template>
        </el-table-column>
        <el-table-column type="index" width="70"></el-table-column>
        <el-table-column property="AppName" label="应用名称" width="120"></el-table-column>
        <el-table-column property="DeviceName" label="设备名称" width="120"></el-table-column>
        <el-table-column property="ServiceId" label="服务Id" width="120"></el-table-column>
        <el-table-column property="ServiceType" label="服务类型" width="120"></el-table-column>
        <el-table-column property="Data" label="数据" min-width="300"></el-table-column>
        <el-table-column property="EventTimeToString" label="上报时间" width="200"></el-table-column>
      </el-table>
      <el-pagination @size-change="handleSizeChange"
                      @current-change="handleCurrentChange"
                      :current-page="currentPage"
                      :page-sizes="pageSizes"
                      :page-size="pageSize"
                      layout="total, sizes, prev, pager, next, jumper"
                      :total="total">
      </el-pagination>      
  </el-card>
</template>
<script>
import {
  getListApi
} from '@/api/historyData'

export default {
  name: 'history-data-list',
  data() {
    return {
      DeviceName: '',
      historyDataItem: {},
      historyDataList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddHistoryData: false,
      addHistoryDataTitle: ''
    }
  },
  computed: {
    cardH() {
      const h =
        window.innerHeight ||
        document.documentElement.clientHeight ||
        document.body.clientHeight
      return {
        height: h - 84 + 'px',
        overflow: 'auto'
      }
    }
  },
  mounted() {
    this.getList()
  },
  methods: {
    handleSizeChange(val) {
      this.pageSize = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.currentPage = val
      this.getList()
    },
    getList() {
      return new Promise((resolve, reject) => {
        getListApi({
          DeviceName: this.DeviceName,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.historyDataList = data.Data
              this.total = data.PageInfo.TotalCount
            }
            resolve()
          })
          .catch(error => {
            reject(error)
          })
      })
    },
    refresh() {
      this.Model = ''
      this.getList()
    }
  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.avatar-uploader {
  .el-upload {
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
}
.avatar-uploader {
  .el-upload:hover {
    border-color: #409eff;
  }
}
.avatar-uploader-icon {
  border: 1px dashed #d9d9d9 !important;
  border-radius: 6px !important;
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>