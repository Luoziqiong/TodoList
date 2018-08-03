<template>
    <div id="easyGrid">
      <div class="easyTotalTable">
        <el-table
          ref="singleTable"
          :data="tableData"
          highlight-current-row
          @selection-change="handleSelectionChange"
          style="width: 100%;" :max-height="height" :height="tableHeight"  @row-click="rowClick" :row-class-name="tableRowClassName"
          @select="selectHead"
          :show-header="showHeader">
          <el-table-column v-if="checkbox"
            type="selection"
            :width="selectionWidth">
          </el-table-column>
          <el-table-column v-for="(n,index) in line" :key="index"
          :min-width="minWidth"
          :type="n.type"
          :property="n.prop"
          :label="n.label"
          :width="n.width">
            <template slot-scope="scope">
              <router-link v-if="n.link" :to="n.link">{{scope.row[n.prop] | filter(filtterValue(scope.row))}}</router-link>
              <label v-else-if="n.img" :class="scope.row[n.prop]"></label>
              <span v-else>{{scope.row[n.prop] | filter(filtterValue(scope.row))}}</span>
            </template>
          </el-table-column>
          <el-table-column v-if="operation.Tboolean"
            fixed="right"
            label="操作"
            :width="operation.width">
              <template slot-scope="scope">              
                  <el-button v-for="(item,index) in scope.row.operate" :key="index" @click="buttonEvent(scope.row,index,item)" size="small" :type="item.type" :disabled="item.isValid">{{item.operateType}}</el-button>
              </template>
            
          </el-table-column>
        </el-table>
        </div>
        <div class="partant">
          <div v-if="tableData.length" class="paging">
            <el-pagination background @current-change="currentChange" :page-size="pageSize" layout="slot, prev, pager, next, jumper" :current-page="presentPage" :total="total">
            <span class="page">共{{total}}条/{{total/pageSize}}页</span>
            </el-pagination>
          </div> 	
        </div>
    </div>
</template>

<script>
export default {
  props: {
    presentPage: {
      //当前页面所在的页码
      type: Number,
      default: 1
    },
    pageSize: {
      //每页显示的的条数
      type: Number,
      default: 10
    },
    total: {
      //分页的总页数
      type: Number,
      default: 0
    },
    operation: {
      //控制操作按钮的显隐
      type: Object,
      default() {
        return {
          width: "",
          Tboolean: false,
          titleLeft: "编辑",
          titleRight: "删除"
        };
      }
    },
    tableData: {
      //表格的数据
      type: Array,
      default() {
        return [];
      }
    },
    line: {
      //表格的列配置
      type: Array,
      default() {
        return [];
      }
    },
    checkbox: {
      //勾选框
      type: Boolean,
      default: false
    },
    minWidth: {
      //设置列的最小宽度
      type: Number,
      default: 120
    },
    height: {
      type: Number,
      default: null
    },
    selectionWidth: {
      //设置勾选框的宽度
      type: String,
      default: "45"
    },
    tableHeight: {
      type: Number,
      default: null
    },
    filtterValue: {
      type: Function,
      default: function() {}
    },
    showHeader: {
      //是否显示表头
      type: Boolean,
      default: true
    }
  },
  filters: {
    filter(value) {
      return value;
    }
  },
  methods: {
    currentChange(val) {
      //当前页数发生变化触发函数
      this.$emit("currentChange", val);
    },
    rowClick(row, event, column) {
      //单击该行触发单击函数
      this.$emit("row-click", row, event, column);
    },
    handleSelectionChange(val) {
      this.$emit("handleSelectionChange", val);
    },
    selectHead(selection, row) {
      //勾选checkbox触发的事件
      this.$emit("select-head", selection, row);
    },
    buttonEvent(row, index, item) {
      this.$emit("button-event", row, index, item);
    },
    tableRowClassName({ row, rowIndex }) {
      if (row.colorSwitch === 1) {
        return "warning-row";
      } else if (row.colorSwitch === 0) {
        return "success-row";
      }
      return "";
    }
  }
};
</script>
<style rel="stylesheet/less">
.el-table__header {
  border-top: 1px solid #eeeeee;
}
tr > th {
  background: #fafafa;
  text-align: center;
}
.paging {
  margin: 0 8px;
}
.partant {
  margin: 8px 0px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.el-table--border th:last-child {
  border: none;
}
div.el-table__empty-block {
  min-height: auto !important;
  width: auto !important;
}

.el-table__row a {
  color: #606266;
  text-decoration: none;
}
.el-table__row a:hover {
  color: #409eff;
}
</style>





