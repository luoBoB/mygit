<template>
  <el-dialog :title='addBannerTitle' :visible.sync='dialogAddBanner' :show-close='false' width='50%'>
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
                <el-form-item label="图片预览" prop="ImageSort">
                    <img :src="preImageUrl" style="width:100%;height:180px;" />
                </el-form-item>
            </el-col>
        </el-row>
    </el-form>
    <div slot='footer' class='dialog-footer'>
      <el-button @click='close'>返 回</el-button>
      <el-button type='primary' @click='saveBanner'>保 存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getNewBannerApi, saveBannerApi } from '@/api/banner'
import { mapGetters } from 'vuex'

export default {
  name: 'add-banner',
  data() {
    return {
      bannerItem: {},
      actionUrl: process.env.BASE_API + '/Upload/UploadFile?name=Banner',
      preImageUrl: '',
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
    ...mapGetters([
      'dialogAddBanner',
      'addBannerTitle',
      'addBannerEditId'
    ])
  },
  watch: {
    dialogAddBanner(newVal, oldVal) {
      if (newVal) {
        this.getNew()
      }
    }
  },
  mounted() {
    console.log(process.env.BASE_API)
  },
  methods: {
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
      this.$store.dispatch('setDialogAddBanner', false)
    }
  }
}
</script>