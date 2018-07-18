<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <!-- <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col> -->
        <el-col :span="7">
            <el-input placeholder="请输入设备名称" v-model="DeviceName">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="userDeviceList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
        <el-table-column type="expand">
            <template slot-scope="props">
              <el-form label-position="left" inline class="demo-table-expand">
                <el-form-item label="设备ID">
                  <span>{{ props.row.DeviceId }}</span>
                </el-form-item>
                <el-form-item label="设备唯一码">
                  <span>{{ props.row.NodeId }}</span>
                </el-form-item>
                <el-form-item label="设备类型">
                  <span>{{ props.row.DeviceType }}</span>
                </el-form-item>
                <el-form-item label="协议类型">
                  <span>{{ props.row.ProtocolType }}</span>
                </el-form-item>
              </el-form>
            </template>
          </el-table-column>
          <el-table-column type="index" width="120"></el-table-column>
          <el-table-column property="DeviceName" label="设备名称" width="200"></el-table-column>
          <el-table-column property="VerifyCode" label="设备验证码" width="200"></el-table-column>
          <el-table-column property="Model" label="设备型号" width="200"></el-table-column>
          <el-table-column property="Phone" label="用户手机号" width="200"></el-table-column>
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
      <el-dialog :title='addUserDeviceTitle' :visible.sync='dialogAddUserDevice' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="userDeviceItem" v-bind:rules="rulesUserDevice" ref="userDeviceItem" label-position="right" label-width="130px">
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备Id" prop="DeviceId">
                        <el-input v-model="userDeviceItem.DeviceId"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备名称" prop="DeviceName">
                        <el-input v-model="userDeviceItem.DeviceName"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备验证码" prop="VerifyCode">
                        <el-input v-model="userDeviceItem.VerifyCode"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备唯一码" prop="NodeId">
                        <el-input v-model="userDeviceItem.NodeId"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备型号" prop="Model">
                        <el-input v-model="userDeviceItem.Model"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="用户手机号" prop="Phone">
                        <el-input v-model="userDeviceItem.Phone"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
                  
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveUserDevice'>保 存</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>
<script>
import { getListApi, getNewApi, addApi, delApi } from '@/api/userDevice'

export default {
  name: 'UserDevice-list',
  data() {
    return {
      DeviceName: '',
      userDeviceItem: {},
      userDeviceList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddUserDevice: false,
      addUserDeviceTitle: '',
      rulesUserDevice: {
        DeviceId: [
          {
            required: true,
            message: '请输入设备Id',
            trigger: 'blur'
          }
        ],
        DeviceName: [
          {
            required: true,
            message: '请输入设备名称',
            trigger: 'blur'
          }
        ],
        VerifyCode: [
          {
            required: true,
            message: '请输入设备验证码',
            trigger: 'blur'
          }
        ],
        NodeId: [
          {
            required: true,
            message: '请输入设备唯一码',
            trigger: 'blur'
          }
        ],
        Model: [
          {
            required: true,
            message: '请输入设备对应的设备型号',
            trigger: 'blur'
          }
        ],
        Phone: [
          {
            required: true,
            message: '请输入设备对应的用户手机号',
            trigger: 'blur'
          }
        ]
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
              this.userDeviceList = data.Data
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
      this.addUserDeviceTitle = '添加用户'
      this.getNew()
    },
    edit(index, row) {
      this.addUserDeviceTitle = '编辑用户'
      this.dialogAddUserDevice = true
      this.userDeviceItem = row
      this.preUserDeviceUrl = process.env.BASE_API + row.Cover
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
      this.DeviceName = ''
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
              this.userDeviceItem = data.Data
              this.dialogAddUserDevice = true
              console.log(this.userDeviceItem)
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
        this.userDeviceItem.Cover = response.Data
        this.preUserDeviceUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveUserDevice: function() {
      console.log(this.userDeviceItem)
      var postData = this.userDeviceItem
      this.$refs.userDeviceItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddUserDevice = false
                  this.userDeviceItem = {}
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
      this.userDeviceItem = {}
      this.dialogAddUserDevice = false
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