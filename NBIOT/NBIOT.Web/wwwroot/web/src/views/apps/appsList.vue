<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入应用名称" v-model="Name">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="appsList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
        <el-table-column type="expand">
            <template slot-scope="props">
              <el-form label-position="left" inline class="demo-table-expand">
                <el-form-item label="应用名称">
                  <span>{{ props.row.Name }}</span>
                </el-form-item>
                <el-form-item label="应用ID">
                  <span>{{ props.row.AppId }}</span>
                </el-form-item>                
                <el-form-item label="应用密钥">
                  <span>{{ props.row.Secret }}</span>
                </el-form-item>
                <el-form-item label="行业">
                  <span>{{ props.row.AppType }}</span>
                </el-form-item>
                <el-form-item label="应用对接码">
                  <span>{{ props.row.AppServer }}</span>
                </el-form-item>
                <el-form-item label="设备对接码">
                  <span>{{ props.row.DeviceServer }}</span>
                </el-form-item>
              </el-form>
            </template>
          </el-table-column>
          <el-table-column type="index" width="120"></el-table-column>
          <el-table-column property="Name" label="应用名称" width="195"></el-table-column>
          <el-table-column property="AppType" label="行业" width="195"></el-table-column>
          <el-table-column property="CreateTimeToString" label="创建时间" width="250"></el-table-column>
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
      <el-dialog :title='addAppsTitle' :visible.sync='dialogAddApps' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="appsItem" v-bind:rules="rulesApps" ref="appsItem" label-position="right" label-width="130px">
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="应用名称" prop="Name">
                        <el-input v-model="appsItem.Name"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="应用ID" prop="AppId">
                        <el-input v-model="appsItem.AppId"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="应用密钥" prop="Secret">
                        <el-input v-model="appsItem.Secret"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">                    
                    <el-form-item label="行业" prop="AppType">
                        <el-select v-model="appsItem.AppType" clearable placeholder="请选择行业" style="width:100%">
                          <el-option label="智慧家庭行业" value="智慧家庭行业">智慧家庭行业</el-option>
                          <el-option label="医疗行业" value="医疗行业">医疗行业</el-option>
                          <el-option label="车联网行业" value="车联网行业">Z-车联网行业</el-option>
                          <el-option label="公用事业" value="公用事业">公用事业</el-option>
                          <el-option label="其它行业" value="其它行业">其它行业</el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>            
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="应用对接码" prop="AppServer">
                        <el-input v-model="appsItem.AppServer"></el-input>
                    </el-form-item>
                </el-col>
            </el-row> 
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="设备对接码" prop="DeviceServer">
                        <el-input v-model="appsItem.DeviceServer"></el-input>
                    </el-form-item>
                </el-col>
            </el-row> 
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveApps'>保 存</el-button>
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
} from '@/api/apps'

export default {
  name: 'apps-list',
  data() {
    return {
      Name: '',
      appsItem: {},
      appsList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddApps: false,
      addAppsTitle: '',
      rulesApps: {
        Name: [{
          required: true,
          message: '请输入应用名称',
          trigger: 'blur'
        }],
        AppId: [{
          required: true,
          message: '请输入应用ID',
          trigger: 'blur'
        }],
        Secret: [{
          required: true,
          message: '请输入应用密钥',
          trigger: 'blur'
        }],
        AppType: [{
          required: true,
          message: '请选择行业',
          trigger: 'blur'
        }],
        AppServer: [{
          required: true,
          message: '请输入应用对接码',
          trigger: 'blur'
        }],
        DeviceServer: [{
          required: true,
          message: '请输入设备对接码',
          trigger: 'blur'
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
    getList() {
      return new Promise((resolve, reject) => {
        getListApi({
          Name: this.Name,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.appsList = data.Data
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
      this.addAppsTitle = '添加APP'
      this.getNew()
    },
    edit(index, row) {
      this.addAppsTitle = '编辑APP'
      this.dialogAddApps = true
      this.appsItem = row
      this.preAppsUrl = process.env.BASE_API + row.Cover
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
      this.Name = ''
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
              this.appsItem = data.Data
              this.dialogAddApps = true
              console.log(this.appsItem)
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
        this.appsItem.Cover = response.Data
        this.preAppsUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveApps: function() {
      console.log(this.appsItem)
      var postData = this.appsItem
      this.$refs.appsItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddApps = false
                  this.appsItem = {}
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
      this.appsItem = {}
      this.dialogAddApps = false
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