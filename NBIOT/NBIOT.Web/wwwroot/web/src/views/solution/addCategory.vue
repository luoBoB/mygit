<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入分类名称" v-model="categoryName">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="categoryList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="index" width="70"></el-table-column>
          <el-table-column property="Logo" label="图标" width="180"></el-table-column>
          <el-table-column property="Name" label="名称" width="250"></el-table-column>
          <el-table-column property="Intro" label="简介" min-width="250"></el-table-column>
          <el-table-column property="SortNo" label="排序" width="100"></el-table-column>
          <el-table-column property="CreateTimeToString" label="创建时间" min-width="200"></el-table-column>
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
      <el-dialog :title='addCategoryTitle' :visible.sync='dialogAddCategory' :show-close='false' :close-on-click-modal="false" width='50%'>
        <el-form v-bind:model="categoryItem" v-bind:rules="rulesCategory" ref="categoryItem" label-position="right" label-width="130px">
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="图标" prop="Logo">
                      <el-input v-model="categoryItem.Logo" placeholder="amazeui的图标样式，例如solution-circle"></el-input>
                  </el-form-item>
              </el-col>
          </el-row>
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="名称" prop="Name">
                      <el-input v-model="categoryItem.Name"></el-input>
                  </el-form-item>
              </el-col>
          </el-row>
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="简介" prop="Intro">
                      <el-input type="textarea" rows="3" v-model="categoryItem.Intro"></el-input>
                  </el-form-item>
              </el-col>
          </el-row>
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="排序" prop="SortNo">
                      <el-input v-model.number="categoryItem.SortNo"></el-input>
                  </el-form-item>
              </el-col>
          </el-row>
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='save'>保 存</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>
<script>
import {
  getCategoryListApi,
  getNewCategoryApi,
  addCategoryApi,
  delCategoryApi
} from '@/api/solution'

export default {
  name: 'add-category',
  data() {
    return {
      categoryName: '',
      categoryItem: {},
      categoryList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddCategory: false,
      addCategoryTitle: '',
      actionUrl:
        process.env.BASE_API + '/Upload/UploadFile?name=SolutionCategory',
      preImageUrl: null,
      rulesCategory: {
        Logo: [
          {
            required: true,
            message: '请输入图标',
            trigger: 'blur'
          }
        ],
        Name: [
          {
            required: true,
            message: '请输入分类名称',
            trigger: 'blur'
          }
        ],
        Intro: [
          {
            required: true,
            message: '请输入简介',
            trigger: 'blur'
          }
        ],
        SortNo: [
          {
            required: true,
            type: 'number',
            message: '请输入排序',
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
    getLogoCls(index, row) {
      return 'am-icon-safari ' + row.Logo
    },
    getList() {
      return new Promise((resolve, reject) => {
        getCategoryListApi({
          Name: this.categoryName,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        })
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.categoryList = data.Data
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
      this.addCategoryTitle = '添加分类'
      this.getNew()
      this.dialogAddCategory = true
    },
    edit(index, row) {
      this.addCategoryTitle = '编辑分类'
      this.dialogAddCategory = true
      this.categoryItem = row
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
            delCategoryApi(row)
              .then(res => {
                const data = res.data
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
      this.categoryName = ''
      this.getList()
    },
    getNew() {
      return new Promise((resolve, reject) => {
        getNewCategoryApi()
          .then(res => {
            const data = res.data
            if (data.Result) {
              this.categoryItem = data.Data
            }
            resolve()
          })
          .catch(error => {
            reject(error)
          })
      })
    },
    save: function() {
      console.log(this.categoryItem)
      var postData = this.categoryItem
      this.$refs.categoryItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            addCategoryApi(postData)
              .then(res => {
                const data = res.data
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddCategory = false
                  this.categoryItem = {}
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
      this.dialogAddCategory = false
    }
  }
}
</script>
