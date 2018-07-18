<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入方案名称" v-model="solutionName">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="solutionList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="index" width="70"></el-table-column>
          <el-table-column label="封面" width="250">
            <template slot-scope="scope">
                <img :src="getUrl(scope.$index, scope.row)" style="width:50%;" />
            </template>
          </el-table-column>
          <el-table-column property="CategoryName" label="所属分类" width="120"></el-table-column>
          <el-table-column property="Title" label="标题" width="120"></el-table-column>
          <el-table-column property="Content" label="内容" width="250" show-overflow-tooltip></el-table-column>
          <el-table-column property="CreateTimeToString" label="创建时间" width="200"></el-table-column>
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
      <el-dialog :title='addSolutionTitle' :visible.sync='dialogAddSolution' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="solutionItem" v-bind:rules="rulesImage" ref="solutionItem" label-position="right" label-width="130px">
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="封面">
                        <el-upload
                          class="avatar-uploader"
                          :action="actionUrl"
                          :show-file-list="false"
                          :on-success="handleSuccess">
                          <img v-if="preImageUrl" :src="preImageUrl" class="avatar">
                          <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                        </el-upload>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="标题" prop="Title">
                        <el-input v-model="solutionItem.Title"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row v-bind:gutter="20">
                <el-col v-bind:span="16">
                    <el-form-item label="分类" prop="CategoryId">
                        <el-select v-model="solutionItem.CategoryId" clearable placeholder="请选择分类" style="width:100%">
                          <el-option
                            v-for="item in categoryOptions"
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
                    <el-form-item label="内容" prop="Content">
                        <el-input type="textarea" rows="6" v-model="solutionItem.Content"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveSolution'>保 存</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>
<script>
import {
  getSolutionListApi,
  getNewSolutionApi,
  addSolutionApi,
  delSolutionApi,
  getCategoryAllApi
} from '@/api/solution'

export default {
  name: 'solution-list',
  data() {
    return {
      solutionName: '',
      solutionItem: {},
      solutionList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddSolution: false,
      addSolutionTitle: '',
      actionUrl: process.env.BASE_API + '/Upload/UploadFile?name=Solution',
      preImageUrl: null,
      categoryOptions: [],
      rulesImage: {
        CategoryId: [{
          required: true,
          message: '请输入所属分类',
          trigger: 'change'
        }],
        Cover: [{
          required: true,
          message: '请输入封面',
          trigger: 'blur'
        }],
        Title: [{
          required: true,
          message: '请输入标题',
          trigger: 'blur'
        }],
        Content: [{
          required: true,
          message: '请输入内容',
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
    this.getCategoryAll()
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
    getCategoryAll() {
      return new Promise((resolve, reject) => {
        getCategoryAllApi()
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.categoryOptions = data.Data
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
        getSolutionListApi({
          Title: this.solutionName,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.solutionList = data.Data
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
      this.addSolutionTitle = '添加解决方案'
      this.getNew()
      this.dialogAddSolution = true
    },
    edit(index, row) {
      this.addSolutionTitle = '编辑解决方案'
      this.dialogAddSolution = true
      this.solutionItem = row
      this.preImageUrl = process.env.BASE_API + row.Cover
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
            delSolutionApi(row)
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
      this.solutionName = ''
      this.getList()
    },
    getUrl(index, row) {
      return process.env.BASE_API + row.Cover
    },
    getNew() {
      return new Promise((resolve, reject) => {
        getNewSolutionApi()
          .then(res => {
            const data = res.data
            console.log(data)
            if (data.Result) {
              this.solutionItem = data.Data
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
        this.solutionItem.Cover = response.Data
        this.preImageUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveSolution: function() {
      console.log(this.solutionItem)
      var postData = this.solutionItem
      this.$refs.solutionItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addSolutionApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddSolution = false
                  this.solutionItem = {}
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
      this.dialogAddSolution = false
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