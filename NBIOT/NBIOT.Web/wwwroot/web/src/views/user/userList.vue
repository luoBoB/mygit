<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入手机号" v-model="Phone">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="userList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="index" width="120"></el-table-column>
          <el-table-column property="Phone" label="账号" width="195"></el-table-column>
          <el-table-column property="AppName" label="所属应用" width="195"></el-table-column>
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
      <el-dialog :title='addUserTitle' :visible.sync='dialogAddUser' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="userItem" v-bind:rules="rulesUser" ref="userItem" label-position="right" label-width="130px">
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="账号" prop="Phone">
                        <el-input v-model="userItem.Phone"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="密码" prop="Password">                        
                        <el-input type="password" v-model="userItem.Password"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="应用名称" prop="AppId">
                        <el-select v-model="userItem.AppId" clearable placeholder="请选择应用" style="width:100%">
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
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveUser'>保 存</el-button>
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
} from '@/api/user'

import {
  getListApi as getAppsListApi
} from '@/api/apps'

export default {
  name: 'User-list',
  data() {
    return {
      Phone: '',
      userItem: {},
      userList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddUser: false,
      addUserTitle: '',
      appsList: [],
      rulesUser: {
        Phone: [{
          required: true,
          message: '请输入账号',
          trigger: 'blur'
        }],
        Password: [{
          required: true,
          message: '请输入密码',
          trigger: 'blur'
        }],
        AppId: [{
          required: true,
          message: '请选择应用',
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
    getList() {
      return new Promise((resolve, reject) => {
        getListApi({
          Phone: this.Phone,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.userList = data.Data
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
      this.addUserTitle = '添加用户'
      this.getNew()
      this.dialogAddUser = true
    },
    edit(index, row) {
      this.getAppsList()
      this.addUserTitle = '编辑用户'
      this.dialogAddUser = true
      this.userItem = row
      this.preUserUrl = process.env.BASE_API + row.Cover
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
      this.Phone = ''
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
              this.userItem = data.Data
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
        this.userItem.Cover = response.Data
        this.preUserUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveUser: function() {
      console.log(this.userItem)
      var postData = this.userItem
      this.$refs.userItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddUser = false
                  this.userItem = {}
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
      this.dialogAddUser = false
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