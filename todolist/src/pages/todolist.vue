<template>
    <div id="todolist">   
        <el-container>
          <el-aside width="170px">
            <el-menu
              :default-active="active"
              class="el-menu-vertical-demo" 
              background-color="#0758ad"
              text-color="#fff"
              active-text-color="#ffd04b"
              @select="handleSelect">
              <el-menu-item index="0">
                <i class="el-icon-menu"></i>
                <span slot="title">全部</span>
              </el-menu-item>
              <el-menu-item index="1"  class="title-i-e">
                <i class="el-icon-menu"></i>
                <span slot="title">很重要-很紧急</span>
              </el-menu-item>
              <el-menu-item index="2"  class="title-i-n">
                <i class="el-icon-menu"></i>
                <span slot="title">重要-不紧急</span>
              </el-menu-item>
              <el-menu-item index="3" class="title-n-e">
                <i class="el-icon-menu"></i>
                <span slot="title">不重要-紧急</span>
              </el-menu-item>
              <el-menu-item index="4" class="title-n-n">
                <i class="el-icon-menu"></i>
                <span slot="title">不重要-不紧急</span>
              </el-menu-item>
            </el-menu>
          </el-aside>
          <el-main>
            <div class="title">
              <div class="title-left">
              <span class="dateTime">
                {{dateTime}}
              </span>
              <el-button type="text" icon="el-icon-arrow-left" size="small" @click="prev"></el-button>
              <el-button type="text" icon="el-icon-arrow-right" size="small" @click="next"></el-button>
              <el-radio-group v-model="radio" size="small" @change="handleSelectChange">
                <el-radio-button label="日"></el-radio-button>
                <el-radio-button label="周"></el-radio-button>
                <el-radio-button label="月"></el-radio-button>
              </el-radio-group>
              </div>
              <div class="title-right">
              <el-select v-model="state" class="state" placeholder="请选择" size="small" @change="handleStateChange">
                <el-option
                  v-for="item in options"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value">
                </el-option>
              </el-select>
              <el-button type="primary" size="small" @click="add">新增</el-button>
              </div>
            </div>
             
           <div class="content">
             <el-collapse accordion>
                <el-collapse-item v-for="(item,index) in todoData" :key="index">
                  <template slot="title">
                    <input type="checkbox" :id="item.id" :checked="item.state===1" 
                    @change="changeState(index,item)"/>
                    <label class="label"
                    :class="{'red-label':item.priority === 1,
                    'orange-label':item.priority === 2,
                    'bule-label':item.priority === 3,
                    'green-label':item.priority === 4}" 
                     :for="item.id"></label>
                    <span class="item-content">{{item.content}}</span>
                  </template>
                  <div>
                    <span class="startDate">
                      <i class="el-icon-date"></i>
                      开始时间：{{item.startDate}}
                    </span>
                    <span class="endDate">
                       <i class="el-icon-date"></i>
                      截止时间：{{item.endDate}}
                    </span>
                  </div>
                  <div class="footer">
                      <el-button @click="edit(index)" size="mini" type="primary">编辑</el-button>
                      <el-button @click="del(item.id)" size="mini" type="danger">删除</el-button>
                  </div>
                </el-collapse-item>
             </el-collapse>
           </div>
          </el-main>
        </el-container>   
        
        <!-- 弹窗相关 -->
        <el-dialog :title="dialogTitle" :visible.sync="dialogVisible" width="450px" >
              <el-form ref="todoForm" :model="todoForm" label-width="80px" size="small">
                <el-form-item label="内容">
                  <el-input v-model="todoForm.content"></el-input>
                </el-form-item>
                <el-form-item label="优先级">
                  <el-select v-model="todoForm.priority" placeholder="请选择优先级">
                    <el-option label="很重要-很紧急" :value="ieVal"></el-option>
                    <el-option label="重要-不紧急" :value="inVal"></el-option>
                    <el-option label="不重要-紧急" :value="neVal"></el-option>
                    <el-option label="不重要-不紧急" :value="nnVal"></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="时间">
                  <el-col :span="11">
                    <el-date-picker type="date" placeholder="选择开始日期" v-model="todoForm.startDate" style="width: 100%;"></el-date-picker>
                  </el-col>
                  <el-col class="line" :span="2">-</el-col>
                  <el-col :span="11">
                    <el-date-picker type="date" placeholder="选择截止日期" v-model="todoForm.endDate" style="width: 100%;"></el-date-picker>
                  </el-col>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button type="primary" @click="submitTodo" size="small">确 定</el-button>
              <el-button @click="cancel" size="small">取 消</el-button>
            </div>
        </el-dialog>
    </div>
</template>
<script>
import MyTable from "@/components/table.vue";
var qs = require("qs");
export default {
  components: {
    MyTable
  },
  data() {
    return {
      active: "1",
      newContent: "",
      todoForm: {},
      ieVal:1,
      inVal:2,
      neVal:3,
      nnVal:4,
      radio:'日',
      dateTime:'',
      today:new Date(),
      todoData: [
        {
          id: 0,
          content: "11111111",
          state: 0,
          priority: 1,
          startDate: "2018-7-7 10:00:00",
          endDate: "2015-5-6 10:00:00"
        },
        {
          id: 1,
          content: "11111111",
          state: 1,
          priority: 2,
          startDate: "2018-7-7 10:00:00",
          endDate: "2015-5-6 10:00:00"
        },
        {
          id: 2,
          content: "11111111",
          state: 0,
          priority: 3,
          startDate: "2018-7-7 10:00:00",
          endDate: "2015-5-6 10:00:00"
        },
        {
          id: 3,
          content: "11111111",
          state: 0,
          priority: 4,
          startDate: "2018-7-7 10:00:00",
          endDate: "2015-5-6 10:00:00"
        }
      ],
      state: 1,
      options: [
        {
          value: 0,
          label: "全部"
        },
        {
          value: 1,
          label: "已完成"
        },
        {
          value: 2,
          label: "未完成"
        }
      ],
      userId: 0,
      dialogTitle: "",
      dialogVisible: false
    };
  },
  created() {
    //let userId = this.$router.query.userId;
    //console.log(userId);
    let date = new Date();
    this.dateTime = this.formateDate(this.today);
    
  },
  methods: {
    handleStateChange(val) {
      console.log(val);
    },
    add() {
      this.dialogTitle = "新增TodoList";
      this.dialogVisible = true;
    },
    handleSelect(index) {
      console.log(index);
      this.active = index;
      console.log(this.active);
    },
    handleSelectChange(){
      console.log(this.radio);
      this.dateTime = this.formateDate(this.today);
    },
    changeState(index,item){
      // console.log(item);
      if(item.state === 1){
        item.state = 0;
      }
      else if(item.state === 0){
        item.state = 1;
      }
      this.todoData.splice(index,1,item);
    },
    submitTodo() {
      this.dialogVisible = false;
      this.todoForm={};
    },
    cancel(){
      this.dialogVisible = false;
      this.todoForm={};
    },
    edit(index) {
      this.dialogTitle = "修改TodoList";
      this.dialogVisible = true;
      let todo = this.todoData[index];
      console.log(todo);
      this.todoForm.content = todo.content;
      this.todoForm.startDate = todo.startDate;
      this.todoForm.endDate = todo.endDate;
      this.todoForm.priority = todo.priority;
    },
    del(id) {
      this.$confirm('此操作将永久删除该记录, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.$message({
            type: 'success',
            message: '删除成功!'
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });          
        });
    },
    prev(){

    },
    next(){

    },
    formateDate(date){
      if(this.radio === '日'){
        return `${date.getFullYear()}-${date.getMonth()+1}-${date.getDate()}`;
      }
      else if(this.radio === '月'){
        return `${date.getFullYear()}-${date.getMonth()+1}`;
      }else{
        let d = date.getDay();
        let firstDate = new Date(date.getFullYear(),date.getMonth(),date.getDate() +1 - d);
        let lastDate = new Date(date.getFullYear(),date.getMonth(),date.getDate() +8 - d);        
        return `${firstDate.getFullYear()}-${firstDate.getMonth()+1}-${firstDate.getDate()}~${lastDate.getDate()+7}`;
      }
    },
    getData() {
      // this.$axios
      //   .get("/api/todolist/", qs.stringify({ userId: this.userId }))
      //   .then(r => {
      //     console.log(r);
      //     if(r.data){
      //         this.$message.success("新增成功");
      //     }
      //     else{
      //         this.$message.error("新增失败");
      //     }
      //   })
      //   .catch(err => {});
    }
  }
};
</script>

<style scoped>
.el-menu {
  height: -webkit-fill-available;
  text-align: left;
}
.el-form {
  text-align: left;
}
.line {
  text-align: center;
}
.state {
  width: 87px;
}
.title{
  text-align: left;
}
.title-left {
  display: inline-block;
  vertical-align: middle;
}
.title-right {
  display: inline-block;
  vertical-align: middle;
  float: right;
}
.title-i-e .el-icon-menu {
  color: #ff4a0e;
}
.title-n-e .el-icon-menu {
  color: #409eff;
}
.title-i-n .el-icon-menu {
  color: #feae6a;
}
.title-n-n .el-icon-menu {
  color: #89c557;
}
.el-main {
  padding: 20px;
}
.content {
  margin-top: 10px;
  padding: 20px;
  text-align: left;
  background: #fff;
}
.footer {
  margin-right: 25px;
  text-align: right;
  
}
.label {
  display: inline-block;
  vertical-align: middle;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  
}
.red-label {
  border: 2px solid #ff4a0e;
}
.orange-label {
  border: 2px solid #feae6a;
}
.bule-label {
  border: 2px solid #409eff;
}
.green-label {
  border: 2px solid #89c557;
}
input[type="checkbox"] {
  display: none;
}

.item-content {
  display: inline-block;
  vertical-align: middle;
}
input[type="checkbox"]:checked + .red-label {
  background: url(../assets/i-red.png) center no-repeat;
}
input[type="checkbox"]:checked + .orange-label {
  background: url(../assets/i-orange.png) center no-repeat;
}
input[type="checkbox"]:checked + .bule-label {
  background: url(../assets/i-bule.png) center no-repeat;
}
input[type="checkbox"]:checked + .green-label {
  background: url(../assets/i-green.png) center no-repeat;
}
.startDate{
  display: inline-block;
  vertical-align: middle;
  margin-left: 25px;
}
.endDate{
  display: inline-block;
  vertical-align: middle;
  margin-left: 100px;
}
.dateTime{
  display: inline-block;
  vertical-align: middle;
  width: 110px;
}
.el-button--text{
  vertical-align: middle;
  font-size: 20px;
}
</style>
