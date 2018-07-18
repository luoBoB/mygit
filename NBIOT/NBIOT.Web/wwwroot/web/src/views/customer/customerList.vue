<template>
  <el-card :style='cardH'>
    <el-row :gutter="5">
        <el-col :span="3">
            <el-button type="success" @click="add">新增商户</el-button>
        </el-col>
        <el-col :span="7">
            <el-input placeholder="请输入商户名称" v-model="customerName">
                <el-button slot="append" icon="search" @click="getList">搜索</el-button>
            </el-input>
        </el-col>
        <el-col :span="6">
            <el-button type="primary" @click="refresh">刷新</el-button>
        </el-col>
      </el-row>
      <el-table :data="customerList" highlight-current-row style="margin-top:5px;height:auto;width:100%">
          <el-table-column type="expand">
              <template slot-scope="scope">
                  <el-form label-position="right" inline class="demo-table-expand">
                      <el-form-item label="负责人电话：">
                          <span>{{ scope.row.JuristicPersonTel }}</span>
                      </el-form-item>
                      <el-form-item label="负责人证件号：">
                          <span>{{ scope.row.CertificateNo }}</span>
                      </el-form-item>
                      <el-form-item label="商户地址：">
                          <span>{{ scope.row.CustomerAddress }}</span>
                      </el-form-item>
                      <el-form-item label="备注说明：">
                          <span>{{ scope.row.Remark }}</span>
                      </el-form-item>
                  </el-form>
              </template>
          </el-table-column>
          <el-table-column type="index" width="70"></el-table-column>
          <el-table-column property="RoleVersionName" label="商户角色" width="120"></el-table-column>
          <el-table-column property="CustomerName" label="商户名称" width="200"></el-table-column>
          <el-table-column property="CustomerTypeName" label="商户类型" width="120"></el-table-column>
          <el-table-column property="JuristicPerson" label="负责人" width="100"></el-table-column>
          <el-table-column property="FatherCustomerName" label="上级代理" min-width="200"></el-table-column>
          <el-table-column property="CreateTimeToString" label="创建时间" min-width="200"></el-table-column>
          <el-table-column label="操作" min-width="250">
              <template slot-scope="scope">
                  <el-button type="success" @click="AddUser(scope.$index, scope.row)" size="mini">添加用户</el-button>
                  <el-button type="success" @click="edit(scope.$index, scope.row)" size="mini">编辑</el-button>
                  <el-button type="warning" @click="del(scope.$index, scope.row)" size="mini">移除</el-button>
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
      <add-customer></add-customer>
  </el-card>
</template>
<script>
import { getListApi } from '@/api/customer'
import addCustomer from './components/addCustomer'

export default {
  name: 'customer-list',
  components: { addCustomer },
  data() {
    return {
      customerName: '',
      customerItem: {},
      customerList: [],
      pageSizes: [10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100],
      pageSize: 10,
      total: 0,
      currentPage: 1,
      dialogAddCustomer: false,
      form: {
        name: '',
        region: '',
        date1: '',
        date2: '',
        delivery: false,
        type: [],
        resource: '',
        desc: ''
      },
      formLabelWidth: '120px'
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
    getList() {
      return new Promise((resolve, reject) => {
        getListApi({
          CustomerName: this.customerName,
          PageIndex: 1,
          PageSize: 10
        }).then(res => {
          const data = res.data
          if (data.Result) {
            this.customerList = data.Data
            this.total = data.PageInfo.TotalCount
          }
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },
    add() {
      this.$store.dispatch('setAddCustomerTitle', '添加商户')
      this.$store.dispatch('setAddCustomerEditId', '')
      this.$store.dispatch('setDialogAddCustomer', true)
    },
    AddUser(index, row) {
      this.$store.dispatch('setDialogAddCustomer', true)
    },
    edit(index, row) {
      this.$store.dispatch('setAddCustomerTitle', '编辑商户')
      this.$store.dispatch('setAddCustomerEditId', row.Id)
      this.$store.dispatch('setDialogAddCustomer', true)
    },
    del(index, row) {
      console.log(index, row)
    },
    refresh() {
      this.customerName = ''
      this.getList()
    }
  }
}
</script>
