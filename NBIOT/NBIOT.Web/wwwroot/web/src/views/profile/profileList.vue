<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入设备型号" v-model="Model">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="profileList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="index" width="80"></el-table-column>
          <el-table-column property="ManufacturerId" label="厂商ID" width="150"></el-table-column>
          <el-table-column property="ManufacturerName" label="厂商名称" width="150"></el-table-column>
          <el-table-column property="DeviceType" label="设备类型" width="150"></el-table-column>
          <el-table-column property="Model" label="设备型号" width="150"></el-table-column>
          <el-table-column property="AppName" label="应用名称" width="150"></el-table-column>
          <el-table-column property="ProtocolType" label="协议类型" width="150"></el-table-column>
          <el-table-column property="ProtocolName" label="解析协议" width="150"></el-table-column>
          <el-table-column label="操作" min-width="250">
              <template slot-scope="scope">
                  <el-button type="success" v-on:click="edit(scope.$index, scope.row)" size="small">编辑</el-button>
                  <el-button type="warning" v-on:click="del(scope.$index, scope.row)" size="small">移除</el-button>
              </template>
          </el-table-column>
      </el-table>
      <el-pagination @size-change="handleSizeChange"
                      @current-change="handleCurrentChange"
                      :current-page="currentPage"
                      :page-sizes="pageSizes"
                      :page-size="pageSize"
                      layout="total, sizes, prev, pager, next, jumper"
                      :total="total">
      </el-pagination>
      <el-dialog :title='addProfileTitle' :visible.sync='dialogAddProfile' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="profileItem" v-bind:rules="rulesProfile" ref="profileItem" label-position="right" label-width="130px">
        <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="厂商ID" prop="ManufacturerId" placeholder="请输入大小写字母、数字">
                        <el-input v-model="profileItem.ManufacturerId"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="厂商名称" prop="ManufacturerName" placeholder="请输入大小写字母、数字">
                        <el-input v-model="profileItem.ManufacturerName"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>

            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备类型" prop="DeviceType">
                        <el-select v-model="profileItem.DeviceType" clearable placeholder="请选择设备类型" style="width:100%">
                          <el-option label="Bulb" value="Bulb">Bulb</el-option>
                          <el-option label="ContactSensor" value="ContactSensor">ContactSensor</el-option>
                          <el-option label="DoorBell" value="DoorBell">DoorBell</el-option>
                          <el-option label="DoorLock" value="DoorLock">DoorLock</el-option>
                          <el-option label="GarageDoor" value="GarageDoor">GarageDoor</el-option>
                          <el-option label="GasMeter" value="GasMeter">GasMeter</el-option>
                          <el-option label="GateWay" value="GateWay">GateWay</el-option>
                          <el-option label="Motion" value="Motion">Motion</el-option>
                          <el-option label="MultiSensor" value="MultiSensor">MultiSensor</el-option>
                          <el-option label="OneButton" value="OneButton">OneButton</el-option>
                          <el-option label="Other" value="Other">Other</el-option>
                          <el-option label="ScopeSensor" value="ScopeSensor">ScopeSensor</el-option>
                          <el-option label="Siren" value="Siren">Siren</el-option>
                          <el-option label="Smoke" value="Smoke">Smoke</el-option>
                          <el-option label="Socket" value="Socket">Socket</el-option>
                          <el-option label="StreetLight" value="StreetLight">StreetLight</el-option>
                          <el-option label="Switch" value="Switch">Switch</el-option>
                          <el-option label="Thermostat" value="Thermostat">Thermostat</el-option>
                          <el-option label="Vehicle" value="Vehicle">Vehicle</el-option>
                          <el-option label="VehicleDetector" value="VehicleDetector">VehicleDetector</el-option>
                          <el-option label="Water" value="Water">Water</el-option>
                          <el-option label="WaterMeter" value="WaterMeter">WaterMeter</el-option>
                          <el-option label="WindowCovering" value="WindowCovering">WindowCovering</el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>

            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备型号" prop="Model" placeholder="请输入大小写字母、数字">                        
                        <el-input type="text" v-model="profileItem.Model"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>                  
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="协议类型" prop="ProtocolType">
                        <el-select v-model="profileItem.ProtocolType" clearable placeholder="请选择协议类型" style="width:100%">
                          <el-option label="CoAP" value="CoAP">CoAP</el-option>
                          <el-option label="HuaweiM2M" value="HuaweiM2M">HuaweiM2M</el-option>
                          <el-option label="Wave" value="Wave">Z-Wave</el-option>
                          <el-option label="ONVIF" value="ONVIF">ONVIF</el-option>
                          <el-option label="WPS" value="WPS">WPS</el-option>
                          <el-option label="Hue" value="Hue">Hue</el-option>
                          <el-option label="WiFi" value="WiFi">WiFi</el-option>
                          <el-option label="J808" value="J808">J808</el-option>
                          <el-option label="Gateway" value="Gateway">Gateway</el-option>
                          <el-option label="ZigBee" value="ZigBee">ZigBee</el-option>
                          <el-option label="LWM2M" value="LWM2M">LWM2M</el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="应用名称" prop="AppId">
                        <el-select v-model="profileItem.AppId" clearable placeholder="请选择应用" style="width:100%">
                          <el-option
                            v-for="item in appsList"
                            :key="item.Id"
                            :label="item.Name"
                            :value="item.Id">
                          </el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>  
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="解析协议名称" prop="ProtocolId">
                        <el-select v-model="profileItem.ProtocolId" clearable placeholder="请选择应用" style="width:100%">
                          <el-option
                            v-for="item in protocolList"
                            :key="item.Id"
                            :label="item.Name"
                            :value="item.Id">
                          </el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row> 
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveProfile'>保 存</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>
<script>
import {
  getListApi,
  getNewApi,
  addApi,
  delApi
} from '@/api/profile'

import {
  getListApi as getAppsListApi
} from '@/api/apps'

import {
  getListApi as getProtocolListApi
} from '@/api/protocol'

export default {
  name: 'profile-list',
  data() {
    return {
      Model: '',
      profileItem: {},
      profileList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddProfile: false,
      addProfileTitle: '',
      appsList: [],
      protocolList: [],
      rulesProfile: {
        ManufacturerId: [{
          required: true,
          message: '请输入厂商Id',
          trigger: 'blur'
        }],
        ManufacturerName: [{
          required: true,
          message: '请输入厂商名称',
          trigger: 'blur'
        }],
        DeviceType: [{
          required: true,
          message: '请选择设备类型',
          trigger: 'change'
        }],
        Model: [{
          required: true,
          message: '请选择设备型号',
          trigger: 'blur'
        }],
        ProtocolType: [{
          required: true,
          message: '请选择协议类型',
          trigger: 'change'
        }],
        AppId: [{
          required: true,
          message: '请选择应用',
          trigger: 'change'
        }],
        ProtocolId: [{
          required: true,
          message: '请选择解析协议',
          trigger: 'change'
        }]
      }
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
    getAppsList() {
      return new Promise((resolve, reject) => {
        getAppsListApi({
          PageIndex: 0,
          PageSize: 0
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.appsList = data.Data
            }
            resolve()
          })
          .catch(error => {
            reject(error)
          })
      })
    },
    getProtocolList() {
      return new Promise((resolve, reject) => {
        getProtocolListApi({
          PageIndex: 0,
          PageSize: 0
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.protocolList = data.Data
            }
            resolve()
          })
          .catch(error => {
            reject(error)
          })
      })
    },
    getList() {
      return new Promise((resolve, reject) => {
        getListApi({
          Model: this.Model,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.profileList = data.Data
              this.total = data.PageInfo.TotalCount
            }
            resolve()
          })
          .catch(error => {
            reject(error)
          })
      })
    },
    add() {
      this.getAppsList()
      this.getProtocolList()
      this.addProfileTitle = '添加profile'
      this.getNew()
      this.dialogAddProfile = true
    },
    edit(index, row) {
      this.getAppsList()
      this.getProtocolList()
      this.addProfileTitle = '编辑profile'
      this.dialogAddProfile = true
      this.profileItem = row
      this.preProfileUrl = process.env.BASE_API + row.Cover
    },
    del(index, row) {
      console.log(index, row)
      this.$confirm('此操作将永久删除该数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          return new Promise((resolve, reject) => {
            delApi(row)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.getList()
                  this.$message.success(data.Message)
                } else {
                  this.$message.error(data.Message)
                }
                resolve()
              })
              .catch(error => {
                reject(error)
              })
          })
        })
        .catch(() => {
          return
        })
    },
    refresh() {
      this.Model = ''
      this.getList()
    },
    getUrl(index, row) {
      return process.env.BASE_API + row.Cover
    },
    getNew() {
      return new Promise((resolve, reject) => {
        getNewApi()
          .then(res => {
            const data = res.data
            console.log(data)
            if (data.Result) {
              this.profileItem = data.Data
            }
            resolve()
          })
          .catch(error => {
            reject(error)
          })
      })
    },
    handleSuccess(response, file, fileList) {
      console.log(file, fileList)
      if (response.Result) {
        this.profileItem.Cover = response.Data
        this.preProfileUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveProfile: function() {
      console.log(this.profileItem)
      var postData = this.profileItem
      this.$refs.profileItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddProfile = false
                  this.profileItem = {}
                } else {
                  this.$message.error(data.Message)
                }
                resolve()
              })
              .catch(error => {
                reject(error)
              })
          })
        } else {
          return false
        }
      })
    },
    close() {
      this.dialogAddProfile = false
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