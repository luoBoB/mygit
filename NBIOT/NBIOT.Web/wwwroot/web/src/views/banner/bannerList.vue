<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入幻灯片名称" v-model="bannerName">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="bannerList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="index" width="70"></el-table-column>
            <el-table-column label="图片" width="250">
              <template slot-scope="scope">
                  <img :src="getUrl(scope.$index, scope.row)" style="width:50%;" />
              </template>
            </el-table-column>
            <el-table-column property="ImageType" label="图片类型" width="120"></el-table-column>
            <el-table-column property="UploadTimeToString" label="上传时间" width="200"></el-table-column>
            <el-table-column property="ImageSort" label="图片排序" width="120"></el-table-column>
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
      <el-dialog :title='addBannerTitle' :visible.sync='dialogAddBanner' :show-close='false' :close-on-click-modal="false" width='50%'>
      <el-form v-bind:model="bannerItem" v-bind:rules="rulesImage" ref="bannerItem" label-position="right" label-width="130px">
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="图片地址" prop="ImageUrl">
                      <el-input v-model="bannerItem.ImageUrl" readonly="true"></el-input>
                  </el-form-item>
              </el-col>
              <el-col v-bind:span="8">
                  <el-upload
                    class="upload-demo"
                    :action="actionUrl"
                    :show-file-list="false"
                    :on-success="handleSuccess">
                    <el-button size="small" type="primary">上传图片</el-button>
                  </el-upload>
              </el-col>
          </el-row>
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="图片排序" prop="ImageSort">
                      <el-input v-model.number="bannerItem.ImageSort"></el-input>
                  </el-form-item>
              </el-col>
          </el-row>
          <el-row v-bind:gutter="20">
              <el-col v-bind:span="16">
                  <el-form-item label="图片预览">
                      <img :src="preImageUrl" style="width:100%;min-height:180px" />
                  </el-form-item>
              </el-col>
          </el-row>
      </el-form>
      <div slot='footer' class='dialog-footer'>
        <el-button @click='close'>返 回</el-button>
        <el-button type='primary' @click='saveBanner'>保 存</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>
<script>
import { getBannerListApi, getNewBannerApi, saveBannerApi, delBannerApi } from '@/api/banner'

export default {
  name: 'banner-list',
  data() {
    return {
      bannerName: '',
      bannerItem: {},
      bannerList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddBanner: false,
      addBannerTitle: '',
      actionUrl: process.env.BASE_API + '/Upload/UploadFile?name=Banner',
      preImageUrl: null,
      rulesImage: {
        ImageUrl: [
          {
            required: true,
            message: '请选择图片上传',
            trigger: 'change'
          }
        ],
        ImageSort: [
          {
            required: true,
            type: 'number',
            message: '请输入一个整数',
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
        height: (h - 84) + 'px',
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
    getUrl(index, row) {
      return process.env.BASE_API + row.ImageUrl
    },
    getList() {
      return new Promise((resolve, reject) => {
        getBannerListApi({
          BannerName: this.bannerName,
          PageIndex: this.currentPage,
          PageSize: this.pageSize
        }).then(res => {
          const data = res.data
          if (data.Result) {
            this.bannerList = data.Data
            this.total = data.PageInfo.TotalCount
          }
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },
    add() {
      this.addBannerTitle = '添加幻灯片'
      this.getNew()
      this.dialogAddBanner = true
    },
    edit(index, row) {
      this.addBannerTitle = '编辑幻灯片'
      this.dialogAddBanner = true
      this.bannerItem = row
      this.preImageUrl = process.env.BASE_API + row.ImageUrl
    },
    del(index, row) {
      console.log(index, row)
      this.$confirm('此操作将永久删除该数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        return new Promise((resolve, reject) => {
          delBannerApi(row)
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
      }).catch(() => {
        return
      })
    },
    refresh() {
      this.bannerName = ''
      this.getList()
    },
    getNew() {
      return new Promise((resolve, reject) => {
        getNewBannerApi()
          .then(res => {
            const data = res.data
            console.log(data)
            if (data.Result) {
              this.bannerItem = data.Data
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
        this.bannerItem.ImageUrl = response.Data
        this.bannerItem.ImageName = file.name
        this.preImageUrl = process.env.BASE_API + response.Data
      } else {
        this.$message.error(response.Message)
      }
    },
    saveBanner: function() {
      console.log(this.bannerItem)
      var postData = this.bannerItem
      this.$refs.bannerItem.validate(valid => {
        if (valid) {
          return new Promise((resolve, reject) => {
            saveBannerApi(postData)
              .then(res => {
                const data = res.data
                console.log(data)
                if (data.Result) {
                  this.$message.success(data.Message)
                  this.getList()
                  this.dialogAddBanner = false
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
      this.dialogAddBanner = false
    }
  }
}
</script>
