<template>
    <div id="todolist">
        <el-form :label-position="labelPosition" label-width="80px" ref="todoForm" :model="todoForm">
            <el-form-item label="内容">
                <el-input v-model="todoForm.content"></el-input>
            </el-form-item>
            <el-form-item label="优先级">
                <el-input v-model="todoForm.priority"></el-input>
            </el-form-item>
            <el-button type="primary" @click="add()">新增</el-button>
           
        </el-form>
        <my-table 
        :tableData="todoData" 
        :line="todoLine" 
        :operation="todoOperation" 
        @handleSelectionChange="todoHandleSelectionChange" 
        @button-event="ButtonEvent" >
        </my-table>
        
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
      todoForm: {
        content: "",
        priority: 0
      },
      todoData: [
        {
          content: "11111111",
          priority: "22222222222",
          state: "333333",
          d: "44444",
          operate: [
            {
              isValid: false,
              operateType: "编辑",
              type: "text",
              value: "edit"
            },
            {
              isValid: false,
              operateType: "删除",
              type: "text",
              value: "remove"
            }
          ]
        }
      ],
      todoLine: [
        { prop: "content", label: "内容" },
        { prop: "priority", label: "优先级" },
        { prop: "state", label: "状态" }
      ],
      todoOperation: {
        Tboolean: true,
        width: 150
      },
      userId: 0
    };
  },
  created() {
      let userId = this.$router.query.userId;
      console.log(userId);

  },
  methods: {
    add() {
      var user = this.todoForm;
      this.axios
        .post(
          "/api/todolist",
          {
            content: this.todoForm.content,
            priority: this.todoForm.priority
          }
        )
        .then(r => {
          console.log(r);
        })
        .catch(err => {});
    },
    getData() {
      this.$axios
        .get("/api/todolist/", qs.stringify({ userId: this.userId }))
        .then(r => {
          console.log(r);
          if(r.data){
              this.$message.success("新增成功");
          }
          else{
              this.$message.error("新增失败");
          }
        })
        .catch(err => {});
    },
    todoHandleSelectionChange(val) {
      console.log(val);
    },
    ButtonEvent(row, index, item) {
      //=======================每个业务再加工处理============================
      const _this = this;
      //console.log(item);
      switch (item.value) {
        case "edit": //投递简历
          console.log(item);
          break;
        case "remove": //取消收藏职位
          _this
            .$confirm("确定不再收藏该职位?", {
              confirmButtonText: "确定",
              cancelButtonText: "取消",
              customClass: "el-message-box-oppositeBtns",
              type: "warning"
            })
            .then(() => {
            this.axios.delete('/api/todolist?id='+ row.id )
                .then(function() {
                console.log(this.$data.list.length);
                  _this.$message({
                    message: "取消收藏成功",
                    type: "success"
                  });
                  _this.getData();
                })
                .catch(function() {
                  _this.$message({
                    message: "取消收藏失败，请联系管理员及时处理",
                    type: "error"
                  });
                });
            })
            .catch(() => {
              this.$message({
                type: "info",
                message: "操作已取消"
              });
            });
          break;
        default:
          break;
      }
    }
  }
};
</script>

<style>
#todolist {
  width: 800px;
  height: 600px;
  margin: auto;
  padding: 50px 20px;
  box-shadow: 1px 1px 10px 0 #eee;
  border-radius: 20px;
}
</style>
