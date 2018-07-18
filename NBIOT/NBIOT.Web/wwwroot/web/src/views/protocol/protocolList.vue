<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入协议名称" v-model="Name">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="protocolList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="index" width="120"></el-table-column>
          <el-table-column property="Name" label="协议名称" width="195"></el-table-column>
          <el-table-column property="DllName" label="DLL名称" width="250"></el-table-column>
          <el-table-column property="DllPath" label="DLL路径" width="250"></el-table-column>
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
      <el-dialog :title='addProtocolTitle' :visible.sync='dialogAddProtocol' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="protocolItem" v-bind:rules="rulesProtocol" ref="protocolItem" label-position="right" label-width="130px">
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="协议名称" prop="Name">
                        <el-input v-model="protocolItem.Name"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="DLL名称" prop="DllName">
                        <el-input v-model="protocolItem.DllName"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="DLL路径" prop="DllPath">
                        <el-input v-model="protocolItem.DllPath"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveProtocol'>保 存</el-button>
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
} from '@/api/protocol'

export default {
  name: 'protocol-list',
  data() {
    return {
      Name: '',
      protocolItem: {},
      protocolList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddProtocol: false,
      addProtocolTitle: '',
      rulesProtocol: {
        Name: [{
          required: true,
          message: '请输入协议名称',
          trigger: 'blur'
        }],
        DllName: [{
          required: true,
          message: '请输入DLL名称',
          trigger: 'blur'
        }],
        DllPath: [{
          required: true,
          message: '请输入DLL路径',
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
              this.protocolList = data.Data
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
      this.addProtocolTitle = '添加协议'
      this.getNew()
    },
    edit(index, row) {
      this.addProtocolTitle = '编辑协议'
      this.dialogAddProtocol = true
      this.protocolItem = row
      this.preProtocolUrl = process.env.BASE_API + row.Cover
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
      this.userName = ''
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
              this.protocolItem = data.Data
              this.dialogAddProtocol = true
              console.log(this.protocolItem)
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
        this.protocolItem.Cover = response.Data
        this.preProtocolUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveProtocol: function() {
      console.log(this.protocolItem)
      var postData = this.protocolItem
      this.$refs.protocolItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddProtocol = false
                  this.protocolItem = {}
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
      this.protocolItem = {}
      this.dialogAddProtocol = false
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