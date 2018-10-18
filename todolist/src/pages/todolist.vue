<template>
    <div id="todolist">   
        <el-container>
          <el-aside width="170px">
            <el-menu
              :default-active="priority"
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
             <el-collapse accordion v-if="todoData.length>0">
                <el-collapse-item v-for="(item,index) in todoData" :key="index">
                  <template slot="title">
                    <input type="checkbox" :id="item.id" :checked="item.stateId===2" 
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
                      截止时间：{{item.finishDate}}
                    </span>
                  </div>
                  <div class="footer">
                      <el-button @click="edit(index)" size="mini" type="primary">编辑</el-button>
                      <el-button @click="del(item.id)" size="mini" type="danger">删除</el-button>
                  </div>
                </el-collapse-item>
             </el-collapse>
            <el-card class="box-card" shadow="never" v-else>
              <div class=" item">
                暂无数据
              </div>
            </el-card>
           </div>
          </el-main>
        </el-container>   
        
        <!-- 弹窗相关 -->
        <el-dialog :title="dialogTitle" :visible.sync="dialogVisible" width="450px" >
              <el-form ref="todoForm" :model="todoForm" label-width="80px" size="small" :rules="todoRules">
                <el-form-item label="内容" prop="content">
                  <el-input v-model="todoForm.content"></el-input>
                </el-form-item>
                <el-form-item label="优先级" prop="priority">
                  <el-select v-model="todoForm.priority" placeholder="请选择优先级">
                    <el-option label="很重要-很紧急" :value="ieVal"></el-option>
                    <el-option label="重要-不紧急" :value="inVal"></el-option>
                    <el-option label="不重要-紧急" :value="neVal"></el-option>
                    <el-option label="不重要-不紧急" :value="nnVal"></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="时间" required>
                  <el-col :span="11">
                    <el-form-item prop="startDate">
                    <el-date-picker type="date" placeholder="选择开始日期" v-model="todoForm.startDate" style="width: 100%;"></el-date-picker>
                    </el-form-item>
                  </el-col>
                  <el-col class="line" :span="2">-</el-col>
                  <el-col :span="11">
                    <el-form-item prop="finishDate">
                    <el-date-picker type="date" placeholder="选择截止日期" v-model="todoForm.finishDate" style="width: 100%;"></el-date-picker>
                    </el-form-item>
                  </el-col>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button type="primary" @click="submitTodo('todoForm')" size="small">确 定</el-button>
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
    var validatStartDate = (rule, value, callback) => {
      if (value == null) {
        callback(new Error("请输入开始时间!"));
      }
      if (value > this.todoForm.finishDate) {
        this.todoForm.startDate = null;
        callback(new Error("开始时间不能大于截止时间!"));
      } else {
        callback();
      }
    };
    var validatFinishDate = (rule, value, callback) => {
      if (value == null) {
        callback(new Error("请输入截止时间!"));
      }
      if (value < this.todoForm.startDate) {
        this.todoForm.finishDate = null;
        callback(new Error("截止时间不能小于开始时间!"));
      } else {
        callback();
      }
    };
    return {
      userId: 0,
      priority: "0",
      todoForm: {},
      todoRules: {
        content: [{ required: true, message: "请输入名称", trigger: "blur" }],
        priority: [
          { required: true, message: "请选择优先级", trigger: "change" }
        ],
        startDate: [
          {
            validator: validatStartDate,
            trigger: "change"
          }
        ],
        finishDate: [
          {
            validator: validatFinishDate,
            trigger: "change"
          }
        ]
      },
      ieVal: 1, //重要-紧急
      inVal: 2, //重要-不紧急
      neVal: 3, //不重要-紧急
      nnVal: 4, //不重要-不紧急
      radio: "日",
      dateTime: "",
      today: new Date(),
      beginTime: "",
      endTime: "",
      todoData: [],
      state: 0,
      options: [
        {
          value: 0,
          label: "全部"
        },
        {
          value: 1,
          label: "未完成"
        },
        {
          value: 2,
          label: "已完成"
        }
      ],
      userId: 0,
      dialogTitle: "",
      dialogVisible: false,
      // 编辑状态描述
      isEdit: false,
      editId: 0
    };
  },
  created() {
    this.userId = this.$route.query.userId;
    let date = new Date();
    this.dateTime = this.formateDate(this.today);
    // 请求列表数据
    this.getData();
  },
  methods: {
    // 状态筛选项改变
    handleStateChange(val) {
      this.getData();
      // console.log(val);
    },
    // 新增
    add() {
      this.dialogTitle = "新增TodoList";
      this.dialogVisible = true;
    },
    // 优先级筛选项改变
    handleSelect(index) {
      console.log(index);
      this.priority = index;
      this.getData();
      console.log(this.priority);
    },
    // 日周月选择
    handleSelectChange() {
      console.log(this.radio);
      this.dateTime = this.formateDate(this.today);
      this.getData();
    },
    // 改变状态
    changeState(index, item) {
      // console.log(item);
      let state = 0;
      if (item.stateId === 1) {
        state = 2;
      } else if (item.stateId === 2) {
        state = 1;
      }
      this.axios
        .post("/api/TodoList/UpdateTodoListState", {
          id: item.id,
          stateId: state
        })
        .then(r => {
          this.getData();
        })
        .catch(err => {});
    },
    // 编辑或新增提交
    submitTodo(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          if (!this.isEdit) {
            this.axios
              .post("/api/TodoList/AddTodoList", {
                content: this.todoForm.content,
                startDate: this.todoForm.startDate,
                finishDate: this.todoForm.finishDate,
                priority: this.todoForm.priority,
                userId: this.userId
              })
              .then(r => {
                this.$message({
                  type: "success",
                  message: "新增成功!"
                });
                this.getData();
              })
              .catch(err => {
                this.$message({
                  type: "error",
                  message: "新增失败!"
                });
              });
          } else {
            this.axios
              .post("/api/TodoList/EditTodoList", {
                id: this.editId,
                content: this.todoForm.content,
                startDate: this.todoForm.startDate,
                finishDate: this.todoForm.finishDate,
                priority: this.todoForm.priority
              })
              .then(r => {
                this.$message({
                  type: "success",
                  message: "修改成功!"
                });
                this.getData();
              })
              .catch(err => {
                this.$message({
                  type: "error",
                  message: "修改失败!"
                });
              });
            this.isEdit = false;
          }
          this.dialogVisible = false;
          this.todoForm = {};
        } else {
          return false;
        }
      });
    },
    cancel() {
      this.dialogVisible = false;
      this.todoForm = {};
    },
    edit(index) {
      this.dialogTitle = "修改TodoList";
      this.dialogVisible = true;
      let todo = this.todoData[index];
      // console.log(todo);
      this.todoForm.content = todo.content;
      this.todoForm.startDate = todo.startDate;
      this.todoForm.finishDate = todo.finishDate;
      this.todoForm.priority = todo.priority;
      this.isEdit = true;
      this.editId = todo.id;
    },
    del(id) {
      this.$confirm("此操作将永久删除该记录, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.axios
            .post(
              "/api/TodoList/DeleteTodoList",
              qs.stringify({
                id: id
              })
            )
            .then(r => {
              this.$message({
                type: "success",
                message: "删除成功!"
              });
              this.getData();
            })
            .catch(err => {});
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    },
    prev() {
      if (this.radio === "日") {
        this.today = new Date(this.today.getTime() - 1000 * 60 * 60 * 24);
      } else if (this.radio === "月") {
        var newDate = new Date(
          this.today.getFullYear(),
          this.today.getMonth() + 1,
          1
        );
        var day = new Date(newDate.getTime() - 1000 * 60 * 60 * 24).getDate();
        this.today = new Date(this.today.getTime() - 1000 * 60 * 60 * 24 * day);
      } else {
        this.today = new Date(this.today.getTime() - 1000 * 60 * 60 * 24 * 7);
      }
      this.dateTime = this.formateDate(this.today);
      this.getData();
    },
    next() {
      if (this.radio === "日") {
        this.today = new Date(this.today.getTime() + 1000 * 60 * 60 * 24);
      } else if (this.radio === "月") {
        var newDate = new Date(
          this.today.getFullYear(),
          this.today.getMonth() + 1,
          1
        );
        var day = new Date(newDate.getTime() - 1000 * 60 * 60 * 24).getDate();
        this.today = new Date(this.today.getTime() + 1000 * 60 * 60 * 24 * day);
      } else {
        this.today = new Date(this.today.getTime() + 1000 * 60 * 60 * 24 * 7);
      }
      this.dateTime = this.formateDate(this.today);
      this.getData();
    },
    formateDate(date) {
      if (this.radio === "日") {
        this.beginTime = date;
        this.endTime = date;
        return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
      } else if (this.radio === "月") {
        this.beginTime = new Date(date.getFullYear(), date.getMonth(), 1);
        // 得到下个月的第一天
        var newDate = new Date(date.getFullYear(), date.getMonth() + 1, 1);
        this.endTime = new Date(newDate.getTime() - 1000 * 60 * 60 * 24);
        return `${date.getFullYear()}-${date.getMonth() + 1}`;
      } else {
        let d = date.getDay();
        let firstDate = new Date(
          date.getFullYear(),
          date.getMonth(),
          date.getDate() - d
        );
        let lastDate = new Date(
          date.getFullYear(),
          date.getMonth(),
          date.getDate() + 6 - d
        );
        this.beginTime = firstDate;
        this.endTime = lastDate;
        return `${firstDate.getFullYear()}-${firstDate.getMonth() +
          1}-${firstDate.getDate()}~${lastDate.getDate()}`;
      }
    },
    getData() {
      this.axios
        .post("/api/TodoList/GetTodoList", {
          beginTime: this.beginTime,
          endTime: this.endTime,
          priority: this.priority,
          stateId: this.state,
          userId: this.userId
        })
        .then(r => {
          // console.log(r);
          this.todoData = r.data;
        })
        .catch(err => {});
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
.title {
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
.startDate {
  display: inline-block;
  vertical-align: middle;
  margin-left: 25px;
}
.endDate {
  display: inline-block;
  vertical-align: middle;
  margin-left: 100px;
}
.dateTime {
  display: inline-block;
  vertical-align: middle;
  width: 110px;
}
.el-button--text {
  vertical-align: middle;
  font-size: 20px;
}
.box-card .item {
  text-align: center;
  font-size: 16px;
  color: #606266;
}
</style>
