<template>
  <el-dialog :title="addCustomerTitle" :visible.sync="dialogAddCustomer" :show-close="false" width="70%">
    <el-form v-bind:model="customerItem" v-bind:rules="rulesCustomer" ref="customerItem" label-position="right" label-width="130px">
        <el-row v-bind:gutter="20">
            <el-col v-bind:xs="12" v-bind:sm="11" v-bind:md="10" v-bind:lg="9">
                <el-form-item label="商户名称" prop="CustomerName">
                    <el-input v-model="customerItem.CustomerName"></el-input>
                </el-form-item>
            </el-col>
            <el-col v-bind:xs="12" v-bind:sm="11" v-bind:md="10" v-bind:lg="9">
                <el-form-item label="商户类型" prop="CustomerType">
                    <el-radio-group v-model.number="customerItem.CustomerType">
                        <el-radio v-bind:label="1">总公司</el-radio>
                        <el-radio v-bind:label="2">商户</el-radio>
                    </el-radio-group>
                </el-form-item>
            </el-col>
            <el-col v-bind:xs="12" v-bind:sm="11" v-bind:md="10" v-bind:lg="9">
                <el-form-item label="商户角色" prop="RoleVersionId">
                    <el-select v-model="customerItem.RoleVersionId" placeholder="请选择" style="width:100%;">
                        <el-option v-for="item in roleVersionOptions"
                                    v-bind:key="item.Id"
                                    v-bind:label="item.Name"
                                    v-bind:value="item.Id">
                        </el-option>
                    </el-select>
                </el-form-item>
            </el-col>
            <el-col v-bind:xs="12" v-bind:sm="11" v-bind:md="10" v-bind:lg="9">
                <el-form-item label="证件号" prop="CertificateNo">
                    <el-input v-model="customerItem.CertificateNo"></el-input>
                </el-form-item>
            </el-col>
        </el-row>
        <el-row v-bind:gutter="20">
            <el-col v-bind:xs="12" v-bind:sm="11" v-bind:md="10" v-bind:lg="9">
                <el-form-item label="负责人" prop="JuristicPerson">
                    <el-input v-model="customerItem.JuristicPerson"></el-input>
                </el-form-item>
            </el-col>
            <el-col v-bind:xs="12" v-bind:sm="11" v-bind:md="10" v-bind:lg="9">
                <el-form-item label="联系方式" prop="JuristicPersonTel">
                    <el-input v-model="customerItem.JuristicPersonTel"></el-input>
                </el-form-item>
            </el-col>
        </el-row>
        <el-row v-bind:gutter="20">
            <el-col v-bind:xs="24" v-bind:sm="22" v-bind:md="20" v-bind:lg="18">
                <el-form-item label="商户地址" prop="CustomerAddress">
                    <el-input v-model="customerItem.CustomerAddress"></el-input>
                </el-form-item>
            </el-col>
        </el-row>
        <el-row v-bind:gutter="20">
        </el-row>
        <el-row v-bind:gutter="20">
            <el-col v-bind:xs="24" v-bind:sm="22" v-bind:md="20" v-bind:lg="18">
                <el-form-item label="备注说明" prop="Remark">
                    <el-input type="textarea" v-bind:rows="5" v-model="customerItem.Remark"></el-input>
                </el-form-item>
            </el-col>
        </el-row>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="close">返 回</el-button>
      <el-button type="primary" @click="saveCustomer">保 存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getNewApi, getRoleVersionOptionsApi } from '@/api/customer'
import { mapGetters } from 'vuex'

export default {
  name: 'add-customer',
  data() {
    return {
      customerItem: {},
      roleVersionOptions: [],
      rulesCustomer: {
        RoleVersionId: [{
          required: true,
          message: '请填写信息',
          trigger: 'blur'
        }],
        CustomerName: [{
          required: true,
          message: '请填写信息',
          trigger: 'blur'
        }],
        JuristicPerson: [{
          required: true,
          message: '请填写信息',
          trigger: 'blur'
        }],
        JuristicPersonTel: [{
          required: true,
          message: '请填写信息',
          trigger: 'blur'
        }],
        CustomerAddress: [{
          required: true,
          message: '请填写信息',
          trigger: 'blur'
        }],
        CustomerType: [{
          required: true,
          type: 'number',
          message: '请填写信息',
          trigger: 'blur'
        }]
      }
    }
  },
  computed: {
    ...mapGetters([
      'dialogAddCustomer',
      'addCustomerTitle',
      'addCustomerEditId'
    ])
  },
  watch: {
    dialogAddCustomer(newVal, oldVal) {
      if (newVal) {
        this.getRoleVersionOptions()
        this.getNew()
      }
    }
  },
  mounted() {

  },
  methods: {
    getNew() {
      return new Promise((resolve, reject) => {
        getNewApi(this.addCustomerEditId).then(res => {
          const data = res.data
          console.log(data)
          if (data.Result) {
            this.customerItem = data.Data
          }
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },
    getRoleVersionOptions: function() {
      return new Promise((resolve, reject) => {
        getRoleVersionOptionsApi().then(res => {
          const data = res.data
          console.log(data)
          if (data.Result) {
            this.roleVersionOptions = data.Data
          }
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },
    saveCustomer: function() {
      console.log('saveCustomer')
      this.$refs.customerItem.validate(function(valid) {
        if (valid) {
          return true
        } else {
          return false
        }
      })
    },
    close() {
      this.$store.dispatch('setDialogAddCustomer', false)
    }
  }
}
</script>